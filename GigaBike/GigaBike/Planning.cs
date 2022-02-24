using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GigaBike {
    public class Planning {
        private List<Week> weeks;

        public Planning() {
            weeks = new List<Week>();
        }

        public void Refresh() {
            // Get information from the databse
        }

        public DateTime GetDeliveryDate(BikeOrder bikeOrder) {
            return new DateTime();
        }

        public List<Slot> GetSlots(int duration) {
            List<Slot> slots = new List<Slot>();
            DateTime currentDate = DateTime.Now;
            int weekNumber = DateCalculator.GetWeekOfYear(currentDate);

            // while (slots.Count == 0) {
                if (IsWeekRegistered(weekNumber, currentDate.Year) == false) AddWeek(weekNumber, currentDate.Year);

                Week currentWeek = GetWeek(weekNumber, currentDate.Year);

                if (currentWeek.IsThereFreeSlotsInWeekFromStartDate(duration, currentDate))
                    slots = currentWeek.GetFreeSlotsFromStartDate(duration, currentDate);

            Trace.WriteLine(currentWeek.IsThereFreeSlotsInWeekFromStartDate(duration, currentDate));

                currentDate = DateCalculator.GoToStartOfNextWeek(currentDate);
            // }

            return slots;
        }

        public List<Week> Weeks {
            get {
                return new List<Week>(weeks);
            }
        }

        private bool IsWeekRegistered(int weekOfYear, int year) {
            foreach (Week currentWeek in weeks) {
                if (currentWeek.WeekNumber == weekOfYear)
                    return true;
            }

            return false;
        }

        private void AddWeek(int weekOfYear, int year) {
            Trace.WriteLine("Add week");
            weeks.Add(new Week(weekOfYear, year));
        }

        private Week GetWeek(int weekOfYear, int year) {
            foreach (Week currentWeek in weeks) {
                if (currentWeek.WeekNumber == weekOfYear)
                    return currentWeek;
            }

            return null;
        }
    }
}
