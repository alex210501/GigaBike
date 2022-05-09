using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class StockBike
    {
        public Bike Bike { get; }
        public int Quantity { get; set; }
        public int IdModel { get; set; }
        public StockBike(Bike bike, int Model,int quantity)
        {
            Bike = bike;
            IdModel = Model;
            Quantity = quantity;
        }
    }
}
