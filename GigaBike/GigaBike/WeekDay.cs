using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class WeekDay {
        private readonly int SlotPerDay = 5;
        private List<Slot> slots;
        public DateTime Date { get; set; }
        public int DayOfWeek { get; }

        public WeekDay(int dayOfWeek) {
            this.DayOfWeek = dayOfWeek;
            slots = new List<Slot>(SlotPerDay);
            Date = new DateTime();
        }

        public bool IsThereFreeSlots(int duration) {
            return false;
        }

        public List<Slot> GetFreeSlots(int duration) {
            return new List<Slot>();
        }

        public List<Slot> Slots {
            get {
                return new List<Slot>(slots);
            }
        }
    }
}
