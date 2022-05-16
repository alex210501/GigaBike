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
        public List<PurchaseOrderPart> Purchases { get; private set; }

        public PurchaseOrderPartHandler(DataBase database) {
            this.database = database;
            currentPurchase = new PurchaseOrderPart();
            Purchases = new List<PurchaseOrderPart>();
        }

        public PurchaseOrderPart CurrentPurchase {
            get {
                return currentPurchase;
            }
        }

        public PurchaseOrderPart GetPurchaseById(int id) {
            return Purchases.Find(p => p.IdPurchaseOrderPart == id);
        }

        public void ClearCurrentPurchase() {
            currentPurchase = new PurchaseOrderPart();
        }

        public void ClearPurchases() {
            Purchases.Clear();
        }

        public void AddPartToCurrentPurchase(Part part, int quantityToOrder) {
            currentPurchase.AddOrderPartWithQuantity(part, quantityToOrder);
        }

        public void SetDateForCurrentPurchase() {
            currentPurchase.orderDate = DateTime.Now.Date;
        }

        public void SaveCurrentOrderToDataBase() {
            SetDateForCurrentPurchase();
            MySqlDataReader reader = database.AddPurchaseOrder(currentPurchase.orderDate);
            reader.Read();
            int idPurchase = reader.GetInt32(0);
            reader.Close();

            foreach(OrderPart orderPart in currentPurchase.OrderParts) {
                // Add the quantity to Order in stock
                orderPart.Part.AddQuantityOrdered(orderPart.QuantityToOrder);
                reader = database.AddPartOrdered(orderPart.Part.IdPart, orderPart.QuantityToOrder);
                reader.Close();

                if (orderPart.QuantityToOrder > 0) {
                    reader = database.AddPurchaseOrderPart(idPurchase, orderPart.Part.IdPart, orderPart.QuantityToOrder);
                    reader.Close();
                }
            }
        }

        public void AddPartToPurchaseOrderById(int idPurchaseOrderPart, Part part, int quantityToOrder, bool isReceived) {
            PurchaseOrderPart purchaseOrderPart = GetPurchaseOrderPartById(idPurchaseOrderPart);

            if (purchaseOrderPart is not null) {
                purchaseOrderPart.AddOrderPartWithQuantity(part, quantityToOrder);
                purchaseOrderPart.IsReceived = isReceived;
            }
        }

        public void GetPurchaseFromDataBase() {
            Purchases.Clear();

            MySqlDataReader reader = database.GetPurchaseOrder();

            while (reader.Read()) {
                int IdPurchaseOrder = reader.GetInt32(0);
                DateTime PurchaseDate = reader.GetDateTime(1);

                PurchaseOrderPart currentPurchaseOrder = new PurchaseOrderPart();
                currentPurchaseOrder.IdPurchaseOrderPart = IdPurchaseOrder;
                currentPurchaseOrder.orderDate = PurchaseDate;

                Purchases.Add(currentPurchaseOrder);
            }
            reader.Close();
        }

        private PurchaseOrderPart GetPurchaseOrderPartById(int idPurchaseOrderPart) {
            return Purchases.Find(p => p.IdPurchaseOrderPart == idPurchaseOrderPart);
        }
    }
}
