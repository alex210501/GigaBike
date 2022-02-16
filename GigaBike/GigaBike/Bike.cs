using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Bike {
        public string Name { get; }
        public int Price { get; }
        public string Color { get; }
        public string Size { get; }

        public Bike (string name, int price, string color, string size) {
            this.Name = name;
            this.Price = price;
            this.Color = color;
            this.Size = size;
        }
    }
}
