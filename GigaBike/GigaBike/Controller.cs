using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace GigaBike {
    public class Controller {
        public Login Login { get; }
        public Catalog Catalog { get; }
        public Order Order { get; }
        public DataBase DataBase { get; }
        public Planning Planning { get; }
        private List<Order> ordersRegistered;

        public Controller() {
            this.DataBase = new DataBase();
            this.Login = new Login(this.DataBase);
            this.Catalog = new Catalog(this.DataBase);
            this.Order = new Order(this.DataBase);
            this.Planning = new Planning(this.DataBase);
            ordersRegistered = new List<Order>();
        }

        public void Init() {
            DataBase.Connect();
        }

        public void SaveOrderAndSlotInDatabase() {
            Order.SaveInDatabase();
            Planning.SaveSlotOfIdOrderToDatabase(Order.IdOrder);
        }

        public void SaveOrderInformation(Customer customer) {
            Planning.RefreshFromDatabase();
            Order.SaveCustomer(customer);
            SetCurrentIdOrder();
            SetDateForOrderBike();
            Order.DeliveryDate = Planning.GetDeliveryDate(Order.IdOrder);
        }

        public void SetCurrentIdOrder() {
            MySqlDataReader reader = DataBase.GetNextIdOrder();
            if (reader.Read()) Order.IdOrder = reader.GetInt32(0);
            reader.Close();
        }

        public void SetDateForOrderBike() {
            Planning.SetSlotForBikeOrder(Order);
        }

        public void RefreshOrderAndPlanningFromDatabase() {
            Planning.RefreshFromDatabase();
            GetOrdersFromDatabase();
            GetOrderModelsFromDatabase();
        }

        public void UpdatePlanningAfterUserUpdate(List<Order> ordersShown) {
            foreach (Order currentOrder in ordersShown)
                currentOrder.Bikes.ForEach(bike => Planning.UpdateSlotsInDatabaseByBikeOrder(bike));
        }

        public List<Order> OrdersRegistered {
            get {
                return new List<Order>(ordersRegistered);
            }
        }

        // TODO: Clean
        private void GetOrdersFromDatabase() {
            MySqlDataReader reader = DataBase.GetOrders();

            while (reader.Read()) {
                int idOrder = reader.GetInt32(0);
                DateTime deliveryDate = reader.GetDateTime(1);
                string nameCustomer = reader.GetString(2);
                string addressCustomer = reader.GetString(3);
                string tvaCustomer = reader.GetString(4);
                string phoneCustomer = reader.GetString(5);

                Customer orderCustomer = new Customer();
                orderCustomer.Name = nameCustomer;
                orderCustomer.Address = addressCustomer;
                orderCustomer.TVA = tvaCustomer;
                orderCustomer.Phone = phoneCustomer;

                Order currentOrder = new Order(DataBase);
                currentOrder.IdOrder = idOrder;
                currentOrder.DeliveryDate = deliveryDate;
                currentOrder.SaveCustomer(orderCustomer);

                ordersRegistered.Add(currentOrder);
            }

            reader.Close();
        }

        private void GetOrderModelsFromDatabase() {
            MySqlDataReader reader = DataBase.GetOrdersModel();

            while (reader.Read()) {
                int idOrder = reader.GetInt32(0);
                int idOrderModel = reader.GetInt32(1);
                int idModelBike = reader.GetInt32(2);
                string nameBike = reader.GetString(3);
                int idColor = reader.GetInt32(4);
                string nameColor = reader.GetString(5);
                int idSize = reader.GetInt32(6);
                string nameSize = reader.GetString(7);
                int priceBike = reader.GetInt32(8);
                int slotDurationBike = reader.GetInt32(9);

                Order currentOrder = ordersRegistered.Find(order => order.IdOrder == idOrder);
                Bike orderBike = new Bike(idModelBike, nameBike, priceBike, new Color(idColor, nameColor), new Size(idSize, nameSize), "", slotDurationBike);
                currentOrder.AddSingleBike(idOrderModel, orderBike);
            }

            reader.Close();

            // Binding to slot
            foreach (Order currentOrder in ordersRegistered) {
                foreach (BikeOrder currentBikeOrder in currentOrder.Bikes)
                    Planning.BindBikeOrderToExistingSlot(currentBikeOrder);
            }
        }
    }
}
