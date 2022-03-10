using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike
{
    public class Part
    {
        public int IdPart { get; }
        //public int Price { get; } //A rajouter
        public string NamePart { get; }
        public int NumberPart { get; }
        public int Threshold { get; }

        public Part(int IdPart, string NamePart, int NumberPart, int Threshold)
        {
            this.IdPart = IdPart;
            this.NamePart = NamePart;
            this.NumberPart = NumberPart;
            this.Threshold = Threshold;
            //this.Price = Price; //A ajouter?
            //this.Location = Location //??
        }

        public Boolean ArePartingStockSufficient() // ajouter les éléments à vérif
        {
            return true; //à ajouter la vérif
        }

    }
}
