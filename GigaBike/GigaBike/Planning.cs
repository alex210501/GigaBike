using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace GigaBike {
    public class Planning {
        private List<Week> weeks;
        private DataBase database;
        private List<Slot> slotToDeleteFromDB;

        public Planning(DataBase database) {
            weeks = new List<Week>();
            this.database = database;
            slotToDeleteFromDB = new List<Slot>();
        }

        public void RefreshFromDatabase() {
            MySqlDataReader reader = database.GetPlanning();

            while (reader.Read()) {
                int idPlanning = reader.GetInt32(0);
                DateTime planningDate = reader.GetDateTime(1);
                int slotNumber = reader.GetInt32(2);
                int idOrderModel = reader.GetInt32(3);
                bool isSlotReady = reader.GetBoolean(4);
                int idOrder = reader.GetInt32(5);
                int idModelBike = reader.GetInt32(6);
                int idColor = reader.GetInt32(7);
                string nameColor = reader.GetString(8);
                int idSize = reader.GetInt32(9);
                string nameSize = reader.GetString(10);

                Slot currentSlot = GetSlotByDateAndSlotNumber(planningDate, slotNumber);
                currentSlot.IdPlanning = idPlanning;
                currentSlot.IsReady = isSlotReady;
                currentSlot.BindSlotWithOrder(idOrder, idOrderModel);
            }

            reader.Close();
        }

        public DateTime GetDeliveryDate(int idOrder) {
            List<Slot> slotOfCurrentOrder = new List<Slot>();

            foreach (Week currentWeek in weeks) slotOfCurrentOrder.AddRange(currentWeek.GetSlotByIdOrderWeek(idOrder));

            return slotOfCurrentOrder.Max(slot => slot.Date);
        }

        public void SetSlotForBikeOrder(Order currentOrder) {
            foreach (BikeOrder bikeOrder in currentOrder.Bikes) {
                // For every order, start searching since tomorrow
                DateTime startSlotDay = DateCalculator.GetNextWorkDayFromToday();

                List<Slot> bikeSlots = GetSlotsFromStartDate(bikeOrder.Bike.SlotDuration, startSlotDay);

                bikeOrder.SetSlotForTheBikeOrder(bikeSlots);

                foreach (Slot slot in bikeSlots) {
                    slot.BindSlotWithOrder(currentOrder.IdOrder, bikeOrder.Bike.IdBike);
                    Trace.WriteLine(string.Format("Slot : {0}: IdOrder : {2}, Date : {1}", slot.SlotNumber, slot.Date, slot.IdOrder)); ;
                }
            }
        }

        public List<Slot> GetSlotsFromStartDate(int duration, DateTime startDate) {
            List<Slot> slots = new List<Slot>();

            // Start searching slot from tomorrow
            DateTime currentDate = startDate;

            while (slots.Count == 0) {
                int weekNumber = DateCalculator.GetWeekOfYear(currentDate);

                Week currentWeek = GetWeek(weekNumber, currentDate.Year);

                if (currentWeek.IsThereFreeSlotsInWeekFromStartDate(duration, currentDate))
                    slots = currentWeek.GetFreeSlotsFromStartDate(duration, currentDate);

                currentDate = DateCalculator.GoToStartOfNextWeek(currentDate);
            }

            return slots;
        }

        public List<Slot> GetAllFreeSlot() {
            List<Slot> freeSlots = new List<Slot>();

            foreach (Week week in weeks) freeSlots.AddRange(week.GetAllFreeSlot());

            return freeSlots;
        }

        public List<Slot> GetFreeSlotFromDate(DateTime currentDate) {
            List<Slot> freeSlotFromDate = new List<Slot>();
            int weekNumber = DateCalculator.GetWeekOfYear(currentDate);

            Week currentWeek = GetWeek(weekNumber, currentDate.Year);
            WeekDay day = currentWeek.Days.Find(day => day.Date == currentDate);

            return day.GetFreeSlots(0);
        }

        public void BindBikeOrderToExistingSlot(BikeOrder bikeOrder) {
            List<Slot> slotOfBikeOrder = GetSlotByIdOrderModel(bikeOrder.IdOrderModel);

            bikeOrder.SetSlotForTheBikeOrder(slotOfBikeOrder);
        }

        public List<Week> Weeks {
            get {
                return new List<Week>(weeks);
            }
        }

        public void SaveSlotOfIdOrderToDatabase(int IdOrder) {
            List<Slot> slotOfOrder = GetSlotByIdOrder(IdOrder);

            if (slotOfOrder.Count > 0) {
                MySqlDataReader reader = database.AddSeveralSlotToPlanning(slotOfOrder);
                reader.Close();
            }
        }

        public void SaveSlotOfIdOrderModelToDatabase(int IdOrder, int IdOrderModel) {
            List<Slot> slotOfOrder = GetSlotByIdOrderModel(IdOrderModel);

            foreach (Slot slot in slotOfOrder) {
                MySqlDataReader reader = database.AddSlotToPlanning(slot, IdOrderModel);
                reader.Close();
            }
        }

        public void UpdateSlotsInDatabaseByBikeOrder(BikeOrder currentBikeOrder) {
            List<Slot> slotBindToOrder = GetSlotByIdOrderModel(currentBikeOrder.IdOrderModel);

            if (slotBindToOrder is not null) {
                foreach(Slot slot in slotBindToOrder) {
                    slot.IsReady = currentBikeOrder.SlotOfBike[0].IsReady;
                    MySqlDataReader reader;

                    if (slot.IdPlanning > 0)
                        reader = database.SetPlanningState(slot.IdPlanning, slot.IsReady);
                    else {
                        reader = database.AddSlotToPlanning(slot, slot.IdOrderModel);
                        if (reader.Read()) slot.IdPlanning = reader.GetInt32(0);
                    }
                    reader.Close();
                }
            }
        }

        public void UnbindSlotByIdOrderModel(int idOrderModel) {
            List<Slot> slotToUnbind = GetSlotByIdOrderModel(idOrderModel);

            slotToUnbind.ForEach(s => s.UnbindSlotFromOrder());
            slotToDeleteFromDB.AddRange(slotToUnbind);
        }


        // TODO: throw exception if not found
        public void BindSlotToIdOrderModelByDuration(int idOrder, int idOrderModel, DateTime date, int slotNumber, int duration =1) {
            for (int i = 0; i < duration; i++) {
                Slot slot = GetSlotByDateAndSlotNumber(date, slotNumber + i);

                if (slot is null || slot.StateSlot == StateSlot.BUSY)
                    throw new Exception("The slot is not found or is busy !");

                slot.BindSlotWithOrder(idOrder, idOrderModel);

                if (slotToDeleteFromDB.Contains(slot)) slotToDeleteFromDB.Remove(slot);
            }
        }

        public void DeleteTheSlotUnusedFromTheDatabase() {
            if (slotToDeleteFromDB.Count > 0)
            {
                MySqlDataReader reader = database.DeleteSeveralSlotFromPlanning(slotToDeleteFromDB);
                reader.Close();

                slotToDeleteFromDB.Clear();
            }
        }

        private bool IsWeekRegistered(int weekOfYear, int year) {
            foreach (Week currentWeek in weeks) {
                if ((currentWeek.WeekNumber == weekOfYear) && (currentWeek.Year == year))
                    return true;
            }

            return false;
        }

        private void AddWeek(int weekOfYear, int year) {
            weeks.Add(new Week(weekOfYear, year));
        }

        private Week GetWeek(int weekOfYear, int year) {
            if (IsWeekRegistered(weekOfYear, year) == false) AddWeek(weekOfYear, year);

            foreach (Week currentWeek in weeks) {
                if ((currentWeek.WeekNumber == weekOfYear) && (currentWeek.Year == year))
                    return currentWeek;
            }

            return null;
        }

        private List<Slot> GetSlotByIdOrder(int IdOrder) {
            List<Slot> slotOfOrder = new List<Slot>();

            foreach (Week currentWeek in weeks) slotOfOrder.AddRange(currentWeek.GetSlotByIdOrderWeek(IdOrder));

            return slotOfOrder;
        }

        private List<Slot> GetSlotByIdOrderModel(int IdOrderModel) {
            List<Slot> slotOfOrder = new List<Slot>();

            foreach (Week currentWeek in weeks) slotOfOrder.AddRange(currentWeek.GetSlotByIdOrderModelWeek(IdOrderModel));

            return slotOfOrder;
        }

        public Slot GetSlotByDateAndSlotNumber(DateTime date, int slotNumber) {
            int weekOfYear = DateCalculator.GetWeekOfYear(date);

            Week currentWeek = GetWeek(weekOfYear, date.Year);

            return currentWeek.GetSlotByDateAndSlotNumber(date, slotNumber);
        }
    }
}
