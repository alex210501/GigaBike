using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class BikeOrder {
        public Bike Bike { get; }
        public int Quantity { get; }
        public List<List<Slot>> slotPerBike;

        public BikeOrder(Bike bike, int quantity) {
            this.Bike = bike;
            this.Quantity = quantity;
            slotPerBike = new List<List<Slot>>();
        }

        public int Price {
            get {
                return (Bike.Price * Quantity);
            }
        }
    }
}
