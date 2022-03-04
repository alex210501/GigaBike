using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GigaBike {
    public class WeekDay {
        private readonly int SlotPerDay = 8;
        private List<Slot> slots;
        public DateTime Date { get; set; }
        public int DayOfWeek { get; }

        public WeekDay(int dayOfWeek, DateTime date) {
            this.DayOfWeek = dayOfWeek;
            this.Date = date;
            slots = new List<Slot>(SlotPerDay);

            // Initialise the slots
            for (int i = 0; i < SlotPerDay; i++)  slots.Add(new Slot(i + 1, this.Date));
        }

        public bool IsThereFreeSlots(int duration) {
            int durationCount = 0;
            
            foreach (Slot currentSlot in slots) {
                durationCount = (currentSlot.StateSlot == StateSlot.FREE) ? (durationCount + 1) : 0;

                if (durationCount == duration) return true;
            }

            return duration == durationCount;
        }

        public List<Slot> GetFreeSlots(int duration) {
            List<Slot> freeSlots = new List<Slot>();

            for (int i = 0; i < (slots.Count) && (freeSlots.Count < duration); i++) {
                Slot currentSlot = slots[i];

                if (currentSlot.StateSlot == StateSlot.FREE) freeSlots.Add(currentSlot);
            }

            return freeSlots;
        }

        public List<Slot> GetSlotByIdOrder(int idOrder) {
            return slots.FindAll(currentSlot => currentSlot.IdOrder == idOrder);
        }

        public List<Slot> GetSlotByIdOrderModel(int idOrderModel) {
            return slots.FindAll(currentSlot => currentSlot.IdOrderModel == idOrderModel);
        }

        public List<Slot> Slots {
            get {
                return new List<Slot>(slots);
            }
        }
    }
}
