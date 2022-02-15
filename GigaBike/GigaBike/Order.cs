using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class Order {
        // public List<Bike> Bikes;
        public int Price { get; }
        public DateTime DateDelivery { get; }
        int Duration { get; set; }

        Order() { }

        void Save(Customer customer) {
            return;
        }

        // void AddBike(Bike)

    }
}
