using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike
{
    public class Part {
        public int IdPart { get; }
        public string NamePart { get; }
        public int QuantityInStock { get; private set; }
        public int QuantityOrdered { get; private set; }
        public int Threshold { get; }
        public int Location { get; }
        public Color Color { get; }
        public Size Size { get; }
        

        public Part(int idPart, string namePart, int quantityInStock, int quantityOrdered, int threshold, int location, Color color, Size size) {
            this.IdPart = idPart;
            this.NamePart = namePart;
            this.QuantityInStock = quantityInStock;
            this.QuantityOrdered = quantityOrdered;
            this.Threshold = threshold;
            this.Location = location;
            this.Color = color;
            this.Size = size;
        }

        public void AddStockQuantity(int quantityToAdd) {
            QuantityInStock += quantityToAdd;
        }

        public void RemoveStockQuantity(int quantityToRemove) {
            QuantityInStock -= quantityToRemove;
            
            if (QuantityInStock < 0)
                QuantityInStock = 0;
        }

        public void AddQuantityOrdered(int quantityOrder) {
            QuantityOrdered += QuantityOrdered;
        }

        public void DeleteQuantityOrdered(int quantityOrder) {
            QuantityOrdered -= quantityOrder;

            if (QuantityOrdered < 0)
                QuantityOrdered = 0;
        }

        public bool ArePartInStockSufficient() {
            return Threshold <= QuantityInStock;
        }
    }
}
