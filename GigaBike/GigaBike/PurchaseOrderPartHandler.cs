using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class PurchaseOrderPartHandler {
        private DataBase database;
        private PurchaseOrderPart currentPurchase;
        private List<PurchaseOrderPart> purchases;

        public PurchaseOrderPartHandler(DataBase database) {
            this.database = database;
            currentPurchase = new PurchaseOrderPart();
            purchases = new List<PurchaseOrderPart>();
        }

        public void ClearCurrentPurchase() {
            currentPurchase = new PurchaseOrderPart();
        }

        public void ClearPurchases() {
            purchases.Clear();
        }

        public void AddPartToCurrentPurchase(Part part, int quantityToOrder) {
            currentPurchase.AddOrderPartWithQuantity(part, quantityToOrder);
        }

        public void RefreshPurchasesFromDatabase() {
            // Get the purchases from the database
        }

        public void SaveCurrentPurchaseToDatabase() {

        }
    }
}
