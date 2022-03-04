using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class BikeOrder {
        public Bike Bike { get; }
        private List<Slot> slotOfBike;

        public BikeOrder(Bike bike) {
            this.Bike = bike;
            slotOfBike = new List<Slot>();
        }

        public void SetSlotForTheBikeOrder(List<Slot> slots){
            slotOfBike.Clear();
            slotOfBike.AddRange(slots);
        }

        public List<Slot> SlotOfBike {
            get {
                return new List<Slot>(slotOfBike);
            }
        }
    }
}
