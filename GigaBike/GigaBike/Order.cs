using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Order {
        private List<Bike> bikes;
        public int Price { get; }
        public DateTime DateDelivery { get; }
        public int Duration { get; set; }

        public Order() {
            bikes = new List<Bike>();
        }

        public void Save(Customer customer) {
            return;
        }

        public void AddBike(Bike bike, int quantity) {
            for(int i = 0; i < quantity; i++) bikes.Add(new Bike(bike));
        }

        public List<Bike> Bikes {
            get {
                return new List<Bike>(bikes);
            }
        }
    }
}
