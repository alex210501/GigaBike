using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike
{
    public class BikePart
    {
        public Part Part { get; }
        public int QuantityForBike { get; set; } = 0;


        public BikePart(Part part, int quantityForBike) {
            Part = part;
            QuantityForBike = quantityForBike;
        }
    }
}
