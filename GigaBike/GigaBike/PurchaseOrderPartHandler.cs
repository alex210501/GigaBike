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
        }
    }
}
