using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GigaBike {
    public class Controller {
        public Login Login { get; }
        public Catalog Catalog { get; }
        public Order Order { get; }
        public DataBase DataBase { get; }
        public Planning Planning { get; }
        public Stock Stock { get; }

        public Controller() {
            this.DataBase = new DataBase();
            this.Login = new Login(this.DataBase);
            this.Catalog = new Catalog(this.DataBase);
            this.Order = new Order(this.DataBase);
            this.Planning = new Planning();
            this.Stock = new Stock(this.DataBase);
        }

        public void Init() {
            DataBase.Connect();
        }

        public void AddToOrder(Bike bike, int quantity) {
            // Add to order sequence
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
