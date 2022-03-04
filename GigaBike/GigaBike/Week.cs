using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GigaBike {
    public class Week {
        private readonly int daysPerWeek = 5;
        public readonly int WeekNumber;
        public readonly int Year;
        private List<WeekDay> days;

        public Week(int weekNumber, int year) {
            this.WeekNumber = weekNumber;
            this.Year = year;
            days = new List<WeekDay>(daysPerWeek);
            CreateDaysList(WeekNumber, Year);
        }
        public List<WeekDay> Days {
            get {
                return new List<WeekDay>(days);
            }
        }

        public bool IsThereFreeSlotsInWeekFromStartDate(int duration, DateTime startDate) {
            // Start searching free slots from a start date
            foreach (WeekDay currentDay in days) {
                if (currentDay.Date >= startDate && currentDay.IsThereFreeSlots(duration))
                    return true;
            }

            return false;
        }

        public List<Slot> GetFreeSlotsFromStartDate(int duration, DateTime startDate) {
            foreach (WeekDay currentDay in days) {
                if (currentDay.Date >= startDate && currentDay.IsThereFreeSlots(duration))
                    return currentDay.GetFreeSlots(duration);
            }

            return null;
        }

        public List<Slot> GetSlotByIdOrderWeek(int idOrder) {
            List<Slot> slotOfCurrentIdOrder = new List<Slot>();

            foreach (WeekDay currentDay in days) slotOfCurrentIdOrder.AddRange(currentDay.GetSlotByIdOrder(idOrder));

            return slotOfCurrentIdOrder;
        }

        public List<Slot> GetSlotByIdOrderModelWeek(int idOrderModel) {
            List<Slot> slotOfCurrentIdOrder = new List<Slot>();

            foreach (WeekDay currentDay in days) slotOfCurrentIdOrder.AddRange(currentDay.GetSlotByIdOrderModel(idOrderModel));

            return slotOfCurrentIdOrder;
        }

        public Slot GetSlotByDateAndSlotNumber(DateTime date, int slotNumber) {
            WeekDay currentDay = days.Find(day => day.Date == date);

            return currentDay.Slots.Find(slot => slot.SlotNumber == slotNumber);
        }

        private void CreateDaysList(int weekOfYear, int year) {
            DateTime currentWeek = DateCalculator.GetDateFromWeekOfYear(weekOfYear, year);

            for (int i = 0; i < daysPerWeek; i++) {
                days.Add(new WeekDay(weekOfYear, currentWeek));
                currentWeek = DateCalculator.GetNextDay(currentWeek);
            }
        }
    }
}
