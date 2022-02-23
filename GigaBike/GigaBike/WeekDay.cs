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

            // Initialise the slots
            for (int i = 0; i < SlotPerDay; i++)  slots[i] = new Slot(i);
        }

        public bool IsThereFreeSlots(int duration) {
            int durationCount = 0;

            foreach (Slot currentSlot in slots) {
                durationCount = (currentSlot.StateSlot == StateSlot.FREE) ? durationCount++ : 0;

                if (durationCount == duration) return true;
            }

            return duration == durationCount;
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
