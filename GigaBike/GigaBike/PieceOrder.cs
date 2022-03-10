using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike
{
    class PieceOrder
    {
        public Part Piece { get; }
        public int NumberPart { get; }

        public PieceOrder(Part piece, int numberPart)
        {
            this.Piece = piece;
            this.NumberPart = numberPart;
        }

        /* a faire quand le prit est definit
        public int Price
        {
            get
            {
                return (Piece.Price * Quantity);
            }
        }
        */
    }
}
