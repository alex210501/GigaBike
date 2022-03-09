using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class BikeOrder {
        public int IdOrderModel { get; set; }
        public Bike Bike { get; }
        private List<Slot> slotOfBike;

        public BikeOrder(Bike bike) {
            this.Bike = bike;
            slotOfBike = new List<Slot>();
        }

        public BikeOrder(BikeOrder otherBike) {
            this.IdOrderModel = otherBike.IdOrderModel;
            this.Bike = new Bike(otherBike.Bike);
            this.slotOfBike = new List<Slot>(otherBike.SlotOfBike);
        }

        public void SetSlotForTheBikeOrder(List<Slot> slots){
            slotOfBike.Clear();
            slotOfBike.AddRange(new List<Slot>(slots));
        }

        public void BindSlotToIdOrderModel() {
            slotOfBike.ForEach(slot => slot.IdOrderModel = IdOrderModel);
        }

        public void SetReadyState(bool state) {
            slotOfBike.ForEach(slot => slot.IsReady = state);
        }

        public List<Slot> SlotOfBike {
            get {
                return new List<Slot>(slotOfBike).ConvertAll(slot => new Slot(slot));
            }
        }
    }
}
