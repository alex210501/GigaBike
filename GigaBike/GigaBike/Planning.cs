﻿using System;
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

        public Planning(DataBase database) {
            weeks = new List<Week>();
            this.database = database;
        }

        public void Refresh() {
            // Get information from the databse
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


                for (int i = 0; i < bikeOrder.Quantity; i++) {
                    List<Slot> bikeSlots = GetSlotsFromStartDate(bikeOrder.Bike.SlotDuration, startSlotDay);

                    bikeOrder.slotPerBike.Add(bikeSlots);

                    foreach (Slot slot in bikeSlots) {
                        slot.BindSlotWithOrder(currentOrder.IdOrder, bikeOrder.Bike.IdBike);
                        Trace.WriteLine(string.Format("Slot : {0}: Date : {1}", slot.SlotNumber, slot.Date));
                    }

                    startSlotDay = bikeSlots[0].Date;
                }
            }
        }

        public List<Slot> GetSlotsFromStartDate(int duration, DateTime startDate) {
            List<Slot> slots = new List<Slot>();

            // Start searching slot from tomorrow
            DateTime currentDate = startDate;

            while (slots.Count == 0) {
                int weekNumber = DateCalculator.GetWeekOfYear(currentDate);

                if (IsWeekRegistered(weekNumber, currentDate.Year) == false) AddWeek(weekNumber, currentDate.Year);

                Week currentWeek = GetWeek(weekNumber, currentDate.Year);

                if (currentWeek.IsThereFreeSlotsInWeekFromStartDate(duration, currentDate))
                    slots = currentWeek.GetFreeSlotsFromStartDate(duration, currentDate);

                // Error on the current date
                currentDate = DateCalculator.GoToStartOfNextWeek(currentDate);
            }

            return slots;
        }

        public List<Week> Weeks {
            get {
                return new List<Week>(weeks);
            }
        }

        public void SaveSlotOfIdOrderModelToDatabase(int IdOrder, int IdOrderModel) {
            List<Slot> slotOfOrder = GetSlotByIdOrderAndIdOrderModel(IdOrder, IdOrderModel);

            foreach (Slot slot in slotOfOrder) {
                MySqlDataReader reader = database.AddSlotToPlanning(slot, IdOrderModel);
                reader.Close();
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
            foreach (Week currentWeek in weeks) {
                if ((currentWeek.WeekNumber == weekOfYear) && (currentWeek.Year == year))
                    return currentWeek;
            }

            return null;
        }

        private List<Slot> GetSlotByIdOrderAndIdOrderModel(int IdOrder, int IdOrderModel) {
            List<Slot> slotOfOrder = new List<Slot>();

            foreach (Week currentWeek in weeks) slotOfOrder.AddRange(currentWeek.GetSlotByIdOrderModelWeek(IdOrderModel));

            return slotOfOrder;
        }

    }
}
