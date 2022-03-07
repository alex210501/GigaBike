using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike
{
    public class Stock
    {
        public int IdStock { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int Threshold { get; set; }
        public int Location { get; set; }

        public Stock() { }

        public Stock(Stock otherStock)
        {
            this.IdStock = otherStock.IdStock;
            this.Name = otherStock.Name;
            this.Number = otherStock.Number;
            this.Threshold = otherStock.Threshold;
            this.Location = otherStock.Location;
        }
    }
}