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

            // Start searching slot from tomorrow
            DateTime currentDate = DateCalculator.GetNextDay(DateTime.Now.Date);

            while (slots.Count == 0) {
                int weekNumber = DateCalculator.GetWeekOfYear(currentDate);

                if (IsWeekRegistered(weekNumber, currentDate.Year) == false) AddWeek(weekNumber, currentDate.Year);

                Week currentWeek = GetWeek(weekNumber, currentDate.Year);

                if (currentWeek.IsThereFreeSlotsInWeekFromStartDate(duration, currentDate))
                    slots = currentWeek.GetFreeSlotsFromStartDate(duration, currentDate);

                currentDate = DateCalculator.GoToStartOfNextWeek(currentDate);
            }

            return slots;
        }

        public List<Week> Weeks {
            get {
                return new List<Week>(weeks);
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
    }
}
