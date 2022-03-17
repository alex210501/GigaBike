using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike
{
    public class PartModel
    {
        public int IdPart { get; }
        //public int Price { get; } //A rajouter
        public int IdModelBike { get; }
        public int NumberPart { get; }
        public int NumberForBike { get; }
        public int Threshold { get; }


        public PartModel(int IdPart, int IdModelBike, int NumberForBike, int NumberPart, int Threshold)
        {
            this.IdPart = IdPart;
            this.IdModelBike = IdModelBike;
            this.NumberPart = NumberPart;
            this.Threshold = Threshold;
            this.NumberForBike = NumberForBike;
            //this.Price = Price; //A ajouter?
        }

        public bool ArePartStockSufficient(int NumberPart, int NumberPartForBike)
        {
            return NumberPart >= NumberPartForBike; //à ajouter la vérif
        }
        public bool AreComponant(int IdBike, int IdPartBike)
        {
            return IdBike == IdPartBike;
        }

    }
}
