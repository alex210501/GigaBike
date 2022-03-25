using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class PurchaseOrderPart {
        public int IdPurchaseOrderPart { get; set; } = 0;
        private List<OrderPart> orderParts;
        public DateTime orderDate { get; set; }

        public PurchaseOrderPart() {
            orderParts = new List<OrderPart>();
        }

        public List<OrderPart> OrderParts {
            get {
                return new List<OrderPart>(orderParts);
            }
        }

        public void ClearOrderParts() {
            orderParts.Clear();
        }

        public void AddOrderPartWithQuantity(Part part, int quantityToOrder) {
            OrderPart orderPartRegistered = orderParts.Find(o => o.Part.IdPart == part.IdPart);

            if (orderPartRegistered is null)
                orderParts.Add(new OrderPart(part, quantityToOrder));
            else
                orderPartRegistered.QuantityToOrder += quantityToOrder;
        }
    }
}
