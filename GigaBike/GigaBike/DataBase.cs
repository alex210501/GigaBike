using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GigaBike {
    public class DataBase {
        private MySqlConnection connection;
        private readonly string host = "pat.ecam.infolab.be";
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
            // A voir, j'ai pas tester
            this.connection = new MySqlConnection(string.Format(@"server={0};user id={1};password={2};persistsecurityinfo=True;database=GigaBike;port={3};sharedmemoryname=", host, username, password, port));
        }

        public MySqlDataReader GetPassword(string username) {
            // Tape la commande pour le mot de passe ici à l'intérieur
            MySqlCommand command = SendCommand("SELECT Password FROM Login WHERE UserName =" + username) ;
            return command.ExecuteReader();
        }

        public MySqlDataReader GetUniqueModel(int modelId) {
            MySqlCommand command = SendCommand("SELECT * FROM BikeModel WHERE IdModel =" + modelId);
            return command.ExecuteReader();
        }

        public MySqlDataReader GetModels() {
            MySqlCommand command = SendCommand("SELECT BikeModel.IdBike, BikeModel.IdModel, Bike.NameBike, BikeModel.IdColor, Color.NameColor, BikeModel.IdSize, Size.NameSize, BikeModel.Price FROM BikeModel INNER JOIN Bike ON Bike.IdBike = BikeModel.IdBike INNER JOIN Color ON Color.IdColor = BikeModel.IdColor INNER JOIN Size ON Size.IdSize = BikeModel.IdSize");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetOrders() {
            MySqlCommand command = SendCommand("SELECT OrderInfo.IdOrder, OrderModel.IdModelBike, OrderInfo.DeliveryDate,Customer.NameCustomer, Customer.Address, Customer.TVA, Customer.Phone FROM OrderInfo INNER JOIN Customer ON Customer.TVA = OrderInfo.TvaCustomer INNER JOIN OrderModel ON OrderModel.IdOrder = OrderInfo.IdOrder");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetCustomer(int customerId) {
            MySqlCommand command = SendCommand("SELECT * FROM Customer WHERE IdCustomer ="+ customerId);
            return command.ExecuteReader();
        }

        public MySqlDataReader GetStock() {
            MySqlCommand command = SendCommand("");
            return command.ExecuteReader();
        }

        public MySqlDataReader SetCustomer(Customer customer) {
            MySqlCommand command = SendCommand("");
            return command.ExecuteReader();
        }

        public MySqlDataReader SaveCommand(Order order) {
            MySqlCommand command = SendCommand("");
            return command.ExecuteReader();
        }
    }
}

