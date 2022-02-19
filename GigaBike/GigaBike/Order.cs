﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Order {
        private List<BikeOrder> bikes;
        public int Price { get; }
        public DateTime DateDelivery { get; }
        public int Duration { get; set; }

        public Order() {
            bikes = new List<BikeOrder>();
        }

        public void Save(Customer customer) {
            return;
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
    }
}
