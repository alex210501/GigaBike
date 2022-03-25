using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GigaBike {
    public class PurchaseOrderPartHandler {
        private DataBase database;
        private PurchaseOrderPart currentPurchase;
        private List<PurchaseOrderPart> purchases;

        public PurchaseOrderPartHandler(DataBase database) {
            this.database = database;
            currentPurchase = new PurchaseOrderPart();
            purchases = new List<PurchaseOrderPart>();
        }

        public PurchaseOrderPart CurrentPurchase {
            get {
                return currentPurchase;
            }
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
        public void SetDateForCurrentPurchase()
        {
            currentPurchase.orderDate = DateTime.Now.Date;
        }
        public void SaveCurrentOrderToDataBase()
        {
            SetDateForCurrentPurchase();
            MySqlDataReader reader = database.AddPurchaseOrder(currentPurchase.orderDate);
            reader.Read();
            int idPurchase = reader.GetInt32(0);
            reader.Close();
            foreach(OrderPart orderPart in currentPurchase.OrderParts)
            {
                if (orderPart.QuantityToOrder > 0)
                {
                    MySqlDataReader reader2 = database.AddPurchaseOrderPart(idPurchase, orderPart.Part.IdPart, orderPart.QuantityToOrder);
                    reader2.Close();
                }
                
            }

            
        }
    }
}
