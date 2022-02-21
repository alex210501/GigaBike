using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Order {
        private List<BikeOrder> bikes;
        public Customer Customer { get; private set; }
        public DateTime DateDelivery { get; }
        public int Duration { get; set; }

        public Order() {
            bikes = new List<BikeOrder>();
            Customer = new Customer();
        }

        public void Save(Customer customer) {
            Customer = new Customer(customer);
        }

        public void Clear() {
            bikes.Clear();
        }

        public void AddBike(Bike bike, int quantity) {
            BikeOrder bikeOrder = new BikeOrder(new Bike(bike), quantity);
            bikes.Add(bikeOrder);
        }

        public List<BikeOrder> Bikes {
            get {
                return new List<BikeOrder>(bikes);
            }
        }

        public int Price {
            get {
                int price = 0;

                foreach (BikeOrder bikeOrder in bikes) price += bikeOrder.Price;

                return price;
            }
        }
    }
}
