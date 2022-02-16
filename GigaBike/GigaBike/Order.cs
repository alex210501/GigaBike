using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Order {
        // public List<Bike> Bikes;
        public int Price { get; }
        public DateTime DateDelivery { get; }
        public int Duration { get; set; }

        public Order() { }

        public void Save(Customer customer) {
            return;
        }

        // void AddBike(Bike)

    }
}
