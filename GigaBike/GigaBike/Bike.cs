using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Bike {
        public int IdBike { get; }
        public string Name { get; }
        public int Price { get; }
        public Color Color { get; }
        public Size Size { get; }

        public Bike (int IdBike, string name, int price, Color color, Size size) {
            this.IdBike = IdBike;
            this.Name = name;
            this.Price = price;
            this.Color = color;
            this.Size = size;
        }
    }
}
