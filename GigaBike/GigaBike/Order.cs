using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace GigaBike {
    public class Order {
        public int IdOrder { get; set; }
        private List<BikeOrder> bikes;
        public Customer Customer { get; private set; }
        public DataBase database { get; }
        public DateTime DeliveryDate { get; set; }
        public int Duration { get; set; }

        public Order(DataBase database) {
            bikes = new List<BikeOrder>();
            Customer = new Customer();
            this.database = database;
        }

        public Order (Order otherOrder) {
            this.IdOrder = otherOrder.IdOrder;
            this.bikes = new List<BikeOrder>(otherOrder.Bikes);
            this.Customer = new Customer(otherOrder.Customer);
            this.database = otherOrder.database;
            this.DeliveryDate = otherOrder.DeliveryDate;
        }

        public void SaveCustomer(Customer customer) {
            Customer = new Customer(customer);
        }

        public void SaveInDatabase() {
            SaveCustomerInDatabase();
            SaveOrderInDatabase();
            SaveBikesOrderInDatabase();
        }

        public void Clear() {
            bikes.Clear();
        }

        public void AddSingleBike(int idOrderModel, Bike bike) {
            BikeOrder currentBikeOrder = new BikeOrder(new Bike(bike));

            currentBikeOrder.IdOrderModel = idOrderModel;
            bikes.Add(currentBikeOrder);
        }

        public void AddBikeByQuantity(Bike bike, int quantity) {
            for (int i = 0; i < quantity; i++)
                bikes.Add(new BikeOrder(new Bike(bike)));
        }

        public List<BikeOrder> Bikes {
            get {
                return new List<BikeOrder>(bikes);
            }
        }

        public int Price {
            get {
                int price = 0;

                foreach (BikeOrder bikeOrder in bikes) price += bikeOrder.Bike.Price;

                return price;
            }
        }

        private void SaveCustomerInDatabase() {
            if (IsCustomerRegistered(Customer) == false) { 
                MySqlDataReader reader = database.SetCustomer(Customer);
                reader.Read();
                reader.Close();
            }
        }

        private bool IsCustomerRegistered(Customer customer) {
            bool isRegistered;

            MySqlDataReader reader = database.GetCustomer(customer.TVA);
            reader.Read();
            isRegistered = reader.HasRows;
            reader.Close();

            return isRegistered;
        }

        private void SaveOrderInDatabase() {
            MySqlDataReader reader = database.SaveCommand(this);
            reader.Read();
            IdOrder = reader.GetInt32(0);
            reader.Close();
        }

        private void SaveBikesOrderInDatabase() {
            int IdOrderModel = 0;

            MySqlDataReader reader = database.AddSeveralOrderModel(this);
            if (reader.Read()) IdOrderModel = reader.GetInt32(0);
            reader.Close();

            foreach (BikeOrder currentBikeOrder in Bikes) {
                currentBikeOrder.IdOrderModel = IdOrderModel++;
                currentBikeOrder.BindSlotToIdOrderModel();
            }
        }
    }
}
