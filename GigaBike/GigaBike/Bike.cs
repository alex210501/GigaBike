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
        public string ImagePath { get; }

        public Bike (int IdBike, string name, int price, Color color, Size size, string imagePath) {
            this.IdBike = IdBike;
            this.Name = name;
            this.Price = price;
            this.Color = color;
            this.Size = size;
            this.ImagePath = imagePath;
        }

        public Bike (Bike otherBike) {
            this.IdBike = otherBike.IdBike;
            this.Name = otherBike.Name;
            this.Price = otherBike.Price;
            this.Color = otherBike.Color;
            this.Size = otherBike.Size;
            this.ImagePath = otherBike.ImagePath;
        }
    }
}
