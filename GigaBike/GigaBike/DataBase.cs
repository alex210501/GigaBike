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
            this.connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public MySqlDataReader GetPasswordAndRole(string username) {
            /*password  =  reader.GetString(0)
              NameRole  =  reader.GetString(1)*/
            string commandToSend = string.Format("SELECT Login.Password, Roles.NameRole FROM Login " +
                                                 "INNER JOIN Roles ON Roles.IdRole = Login.IdRole " +
                                                 "WHERE UserName = "+'"'+username+'"');
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader GetUniqueModel(int modelId) {
            /*IdModel  =  reader.GetInt(0)
              IdBkie  =  reader.GetInt(1)
              IdColor  =  reader.GetString(2)
              IdSize  =  reader.GetInt(3)
              Price  =  reader.GetInt(4)
              PicturePath  =  reader.GetString(5)*/
            MySqlCommand command = SendCommand("SELECT * FROM BikeModel WHERE IdModel =" + modelId);
            return command.ExecuteReader();
        }

        public MySqlDataReader GetModels() {
            MySqlCommand command = SendCommand("SELECT BikeModel.IdModel, BikeModel.IdBike, Bike.NameBike, BikeModel.IdColor, Color.NameColor, BikeModel.IdSize, Size.NameSize, BikeModel.Price, BikeModel.PicturePath, BikeModel.SlotDuration FROM BikeModel " +
                                                "INNER JOIN Bike ON Bike.IdBike = BikeModel.IdBike " +
                                                "INNER JOIN Color ON Color.IdColor = BikeModel.IdColor " +
                                                "INNER JOIN Size ON Size.IdSize = BikeModel.IdSize");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetOrders() {
            MySqlCommand command = SendCommand("SELECT OrderInfo.IdOrder, OrderModel.IdModelBike, OrderInfo.DeliveryDate,Customer.NameCustomer, Customer.AddressCustomer, Customer.TVA, Customer.PhoneCustomer FROM OrderInfo " +
                                               "INNER JOIN Customer ON Customer.TVA = OrderInfo.TvaCustomer " +
                                               "INNER JOIN OrderModel ON OrderModel.IdOrder = OrderInfo.IdOrder");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetCustomer(string tva) {
            /*TVA  =  reader.GetString(0)
              NameCustomer  =  reader.GetString(1)
              AddressCustomer  =  reader.GetString(2)
              PhoneCustomer  =  reader.GetString(3)
            */
            MySqlCommand command = SendCommand(string.Format("SELECT * FROM Customer WHERE TVA=\"{0}\";", tva));
            return command.ExecuteReader();
        }

        public MySqlDataReader GetStock() {
            /*IdpartModelidpartModel = reader.GetInt(0)
             *IdPart = reader.GetInt(1)
             *IdModel = reader.GetInt(2)
             *NumbrForBike = reader.GetInt(3)
             * NamePart =  reader.GetString(5)
             * IdPartColor = reader.GetInt(6)
             * IdPartSize = reader.GetInt(7)
             * NumberPartInStock = reader.GetInt(8)
             * threshold = reader.GetInt(9)
             * Location = reader.GetInt(10)
             * Colorname =  reader.GetString(11)
             * Sizename =  reader.GetString(12)
            */
            MySqlCommand command = SendCommand("SELECT PartModel.*, Part.*, Color.NameColor, Size.NameSize From PartModel " +
                                                "Inner Join Part ON Part.IdPart = PartModel.IdPart " +
                                                "Inner Join Color ON Color.IdColor = Part.IdPartColor " +
                                                "Inner Join Size ON Size.IdSize = Part.IdPartSize" );
            return command.ExecuteReader();
        }
        public MySqlDataReader GetPartStock()
        {
            /*IdPart = reader.GetInt(0)
             *NamePart = reader.GetInt(1)
             *IdPartColor = reader.GetInt(2)
             *IdPartSize = reader.GetInt(3)
             *NumberPartInStock =  reader.GetString(4)
             *Threshold = reader.GetInt(5)
             *Location = reader.GetInt(6)
            */
            MySqlCommand command = SendCommand("SELECT * From Part ");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetNextIdOrder() {
            string commandToSend = "SELECT AUTO_INCREMENT FROM information_schema.Tables " +
                                   "WHERE TABLE_SCHEMA = \"GigaBike\" " +
                                   "AND TABLE_NAME = \"OrderInfo\";";
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader SetCustomer(Customer customer) {
            string commandToSend = string.Format("INSERT INTO Customer (TVA, NameCustomer, AddressCustomer, PhoneCustomer) VALUES (\"{0}\", \"{1}\", \"{2}\", \"{3}\");",
                                                 customer.TVA, customer.Name, customer.Address, customer.Phone);
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader SaveCommand(Order order) {
            string commandToSend = string.Format("INSERT INTO OrderInfo (TVACustomer, Price) VALUES (\"{0}\",\"{1}\");" +
                                                 "SELECT @@IDENTITY;", order.Customer.TVA, order.Price);
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader SaveCommandModels(int idOrder, BikeOrder currentBikeOrder) {
            string commandToSend = string.Format("INSERT INTO OrderModel(IdOrder, IdModelBike) VALUES (\"{0}\",\"{1}\");" +
                                                 "SELECT @@IDENTITY;", idOrder, currentBikeOrder.Bike.IdBike);
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }
        public MySqlDataReader GetPlanning()
        {
            /*IdPlanning =  reader.GetInt(0)
              PlanningDate =  reader.GetString(1)
              Slot  =  reader.GetInt(2)
              OrderModel  =  reader.GetInt(3)
              IsReady  =  reader.GetString(4)
              IdOrder  =  reader.GetInt(5)
              IdModelBike  =  reader.GetInt(6)
            */
            MySqlCommand command = SendCommand("SELECT Planning.*, OrderModel.IdOrder, OrderModel.IdModelBike FROM Planning " +
                                                "INNER JOIN OrderModel ON OrderModel.IdOrderModel = Planning.OrderModel");
            return command.ExecuteReader();
        }
        public MySqlDataReader GetPartModel()
        {
            /*IdPartModel  =  reader.GetInt(0)
              IdPart  =  reader.GetInt(1)
              IdModel  =  reader.GetInt(2)
              NumberForBike  =  reader.GetInt(3)
              NamePart  =  reader.GetString(4)
              IdPartColor  =  reader.GetInt(5)
              IdPartSize  =  reader.GetInt(6)
              NumberPartsInStock  =  reader.GetInt(7)
              Threshold  =  reader.GetInt(8)
              location  =  reader.GetInt(9)
            */
            MySqlCommand command = SendCommand("SELECT * FROM PartModel INNER JOIN Part ON Part.IdPart = PartModel.IdPart");
            return command.ExecuteReader();
        }
        public MySqlDataReader SetPlanningState(int IdPlanning, bool State)
        {
            // State accept only true or false
            MySqlCommand command = SendCommand("UPDATE Planning SET IsReady = " + State + " where IdPlanning = "+IdPlanning);
            return command.ExecuteReader();
        }
        public MySqlDataReader AddPartModelToStock(int IdPart, int QuantityToAdd)
        {
            //add a quantity to Part.NumberPartInStock
            
            MySqlCommand command = SendCommand("UPDATE Part set NumberPartInStock = NumberPartInStock+" + QuantityToAdd+ " WHERE IdPart=" + IdPart);
            return command.ExecuteReader();
        }

    }
}

