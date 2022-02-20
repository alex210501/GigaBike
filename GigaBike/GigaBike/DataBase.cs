using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Net;

namespace GigaBike {
    public class DataBase {
        private MySqlConnection connection;
        private readonly string host = "pat.infolab.ecam.be";
        private readonly string database = "GigaBike";
        private readonly string username = "gigabike";
        private readonly string password = "0123456789";
        private readonly int port = 63313;
        public bool IsConnected { get; }

        public DataBase() { }

        private MySqlCommand SendCommand(string command) {
            return new MySqlCommand(command, connection);
        }

        public void Connect() {
            // Code to connect to the database
            string connectionString = string.Format(@"server={0};userid={1};pwd={2};persistsecurityinfo=True;database={3};port={4};sharedmemoryname=", host, username, password, database, port);
            this.connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            connection.Open();
        }

        public MySqlDataReader GetPassword(string username) {
            // Tape la commande pour le mot de passe ici à l'intérieur
            string commandToSend = string.Format("SELECT Password FROM Login WHERE UserName={0}", username);
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader GetUniqueModel(int modelId) {
            MySqlCommand command = SendCommand("SELECT * FROM BikeModel WHERE IdModel =" + modelId);
            return command.ExecuteReader();
        }

        public MySqlDataReader GetModels() {
            MySqlCommand command = SendCommand("SELECT BikeModel.IdBike, BikeModel.IdModel, Bike.NameBike, BikeModel.IdColor, Color.NameColor, BikeModel.IdSize, Size.NameSize, BikeModel.Price FROM BikeModel " +
                                                "INNER JOIN Bike ON Bike.IdBike = BikeModel.IdBike " +
                                                "INNER JOIN Color ON Color.IdColor = BikeModel.IdColor " +
                                                "INNER JOIN Size ON Size.IdSize = BikeModel.IdSize");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetOrders() {
            MySqlCommand command = SendCommand("SELECT OrderInfo.IdOrder, OrderModel.IdModelBike, OrderInfo.DeliveryDate,Customer.NameCustomer, Customer.Address, Customer.TVA, Customer.Phone FROM OrderInfo " +
                                               "INNER JOIN Customer ON Customer.TVA = OrderInfo.TvaCustomer " +
                                               "INNER JOIN OrderModel ON OrderModel.IdOrder = OrderInfo.IdOrder");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetCustomer(int customerId) {
            MySqlCommand command = SendCommand("SELECT * FROM Customer WHERE IdCustomer ="+ customerId);
            return command.ExecuteReader();
        }

        public MySqlDataReader GetStock() {
            MySqlCommand command = SendCommand("SELECT * FROM Part");
            return command.ExecuteReader();
        }

        public MySqlDataReader SetCustomer(Customer customer) {
            /*string command = string.Format("INSERT INTO Customer VALUES ({0},{1},{2},{3})", customer.);
            MySqlCommand command = SendCommand(,customer.TVA,'"'+customer.NameCustomer+'"','"'+customer.Address+'"',customer.Phone);*/
            MySqlCommand command = SendCommand("");
            return command.ExecuteReader();
        }

        public MySqlDataReader SaveCommand(Order order) {
            // MySqlCommand command = SeyyndCommand("INSERT INTO OrderInfo VALUES (Null,{0},{1},{2},{3})",order.TvaCustomer,"'"+order.DeliveryDate+"'","'"+order.Duration+"'",order.Price);
            MySqlCommand command = SendCommand("");
            return command.ExecuteReader();
        }
    }
}

