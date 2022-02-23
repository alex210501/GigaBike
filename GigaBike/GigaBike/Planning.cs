using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class Planning {
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
            weeks.Add(new Week(weekOfYear, year));
        }
    }
}
