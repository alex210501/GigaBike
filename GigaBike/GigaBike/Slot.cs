using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {

    enum StateSlot {
        BUSY = 0,
        FREE = 1
    }

    class Slot {
        public readonly int SlotNumber;
        public StateSlot StateSlot { get; private set; }
        public int IdOrder { get; set; }
        public int IdOrderModel { get; set; }
        public DateTime Date { get; private set; }

        public Slot(int slotNumber) {
            this.SlotNumber = slotNumber;
            StateSlot = StateSlot.FREE;
        }

        public void BindSlotWithOrder(int idOrder, int idOrderModel) {
            IdOrder = idOrder;
            IdOrderModel = idOrderModel;
        }
    }
}
