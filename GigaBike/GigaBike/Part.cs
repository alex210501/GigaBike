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
        public int Location { get; }
        

        public Part(int IdPart, string NamePart, int NumberPart, int Threshold, int Location)
        {
            this.IdPart = IdPart;
            this.NamePart = NamePart;
            this.NumberPart = NumberPart;
            this.Threshold = Threshold;
            this.Location = Location;
            //this.Price = Price; //A ajouter?
            // //??
        }

        public Boolean ArePartingStockSufficient() // ajouter les éléments à vérif
        {
            return true; //à ajouter la vérif
        }

    }
}
