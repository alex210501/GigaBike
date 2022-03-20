using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class OrderPart {
        public Part Part { get; }
        public int QuantityToOrder { get; set; }

        public OrderPart (Part part, int quantityToOrder) {
            Part = part;
            QuantityToOrder = quantityToOrder;
        }
    }
}
