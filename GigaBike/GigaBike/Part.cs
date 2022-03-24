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
        public int QuantityInStock { get; }
        public int Threshold { get; }
        public int Location { get; }
        public Color Color { get; }
        public Size Size { get; }
        

        public Part(int idPart, string namePart, int quantityInStock, int threshold, int location, Color color, Size size) {
            this.IdPart = idPart;
            this.NamePart = namePart;
            this.QuantityInStock = quantityInStock;
            this.Threshold = threshold;
            this.Location = location;
            this.Color = color;
            this.Size = size;
            //this.Price = Price; //A ajouter?
            // //??
        }

        public Boolean ArePartingStockSufficient() // ajouter les éléments à vérif
        {
            return true; //à ajouter la vérif
        }

        public bool ArePartInStockSufficient() {
            return Threshold <= QuantityInStock;
        }
    }
}
