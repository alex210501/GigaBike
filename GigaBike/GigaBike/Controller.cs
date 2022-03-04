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

        public Controller() {
            this.DataBase = new DataBase();
            this.Login = new Login(this.DataBase);
            this.Catalog = new Catalog(this.DataBase);
            this.Order = new Order(this.DataBase);
            this.Planning = new Planning(this.DataBase);
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
    }
}
