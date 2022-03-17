using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {

    public enum StateSlot {
        BUSY = 0,
        FREE = 1
    }

    public class Slot {
        public readonly int SlotNumber;
        public StateSlot StateSlot { get; private set; }
        public int IdPlanning { get; set; }
        public int IdOrder { get; set; }
        public int IdOrderModel { get; set; }
        public DateTime Date { get; private set; }
        public bool IsReady { get; set; }

        public Slot(int slotNumber, DateTime date) {
            this.SlotNumber = slotNumber;
            this.Date = date;
            StateSlot = StateSlot.FREE;
            IdPlanning = 0;
            IdOrder = 0;
            IdOrderModel = 0;
        }

        public Slot(Slot otherSlot) {
            this.IdPlanning = otherSlot.IdPlanning;
            this.IdOrder = otherSlot.IdOrder;
            this.IdOrderModel = otherSlot.IdOrderModel;
            this.SlotNumber = otherSlot.SlotNumber;
            this.Date = otherSlot.Date;
            this.IsReady = otherSlot.IsReady;
        }

        public void BindSlotWithOrder(int idOrder, int idOrderModel) {
            IdOrder = idOrder;
            IdOrderModel = idOrderModel;
            StateSlot = StateSlot.BUSY;
        }

        public void UnbindSlotFromOrder() {
            IdPlanning = 0;
            IdOrder = 0;
            IdOrderModel = 0;
            StateSlot = StateSlot.FREE;
        }
    }
}
