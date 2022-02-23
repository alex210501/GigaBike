using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class Week {
        private readonly int daysPerWeek = 5;
        public readonly int WeekNumber;
        public readonly int Year;
        private List<WeekDay> days;

        public Week(int weekNumber, int year) {
            this.WeekNumber = weekNumber;
            this.Year = year;
            days = new List<WeekDay>(daysPerWeek);
        }
        public List<WeekDay> Days {
            get {
                return new List<WeekDay>(days);
            }
        }

        public bool IsThereFreeSlotsInWeekFromStartDate(int duration, DateTime startDate) {
            // Start searching free slots from a start date
            foreach (WeekDay day in days) {
                if (day.Date >= startDate && day.IsThereFreeSlots(duration) && day.IsThereFreeSlots(duration))
                    return true;
            }

            return false;
        }

        public List<Slot> GetFreeSlots(int duration) {
            return new List<Slot>();
        }
    }
}
