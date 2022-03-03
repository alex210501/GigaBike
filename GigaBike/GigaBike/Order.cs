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
        private DataBase database;
        public DateTime DateDelivery { get; }
        public int Duration { get; set; }

        public Order(DataBase database) {
            bikes = new List<BikeOrder>();
            Customer = new Customer();
            this.database = database;
        }

        public void Save(Customer customer) {
            Customer = new Customer(customer);

            SaveCutomer(customer);
        }

        public void Validate() {
            MySqlDataReader reader = database.SaveCommand(this);
            reader.Read();
            IdOrder = reader.GetInt32(0);
            reader.Close();

            foreach (BikeOrder currentBikeOrder in bikes) {
                for (int i = 0; i < currentBikeOrder.Quantity; i++) {
                    MySqlDataReader bikeOrderReader = database.SaveCommandModels(IdOrder, currentBikeOrder.Bike);
                    bikeOrderReader.Close();
                }
            }
        }

        public void Clear() {
            bikes.Clear();
        }

        public void AddBike(Bike bike, int quantity) {
            BikeOrder bikeOrder = new BikeOrder(new Bike(bike), quantity);
            bikes.Add(bikeOrder);
        }

        public List<BikeOrder> Bikes {
            get {
                return new List<BikeOrder>(bikes);
            }
        }

        public int Price {
            get {
                int price = 0;

                foreach (BikeOrder bikeOrder in bikes) price += bikeOrder.Price;

                return price;
            }
        }

        private void SaveCutomer(Customer customer) {
            Trace.WriteLine(customer.Name);

            if (IsCustomerRegistered(customer) == false) { 
                MySqlDataReader reader = database.SetCustomer(customer);
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
    }
}
