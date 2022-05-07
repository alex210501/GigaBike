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
            MySqlCommand command = SendCommand("SELECT OrderInfo.IdOrder, OrderInfo.DeliveryDate,Customer.NameCustomer, Customer.AddressCustomer, Customer.TVA, Customer.PhoneCustomer FROM OrderInfo " +
                                               "INNER JOIN Customer ON Customer.TVA = OrderInfo.TvaCustomer ");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetOrdersModel() {
            string commandToSend = "SELECT OrderModel.IdOrder, OrderModel.IdOrderModel, OrderModel.IdModelBike, Bike.NameBike, BikeModel.IdColor," +
                                   "Color.NameColor, BikeModel.IdSize, Size.NameSize, BikeModel.Price, BikeModel.SlotDuration FROM OrderModel " +
                                   "INNER JOIN BikeModel ON OrderModel.IdModelBike = BikeModel.IdModel " +
                                   "INNER JOIN Color ON Color.IdColor = BikeModel.IdColor " +
                                   "INNER JOIN Size ON Size.IdSize = BikeModel.IdSize " +
                                   "INNER JOIN Bike ON Bike.IdBike = BikeModel.IdBike;";
            MySqlCommand command = SendCommand(commandToSend);

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
            /*idPartModel = reader.GetInt(0)
             *IdPart = reader.GetInt(1)
             *IdModel = reader.GetInt(2)
             *NumbrForBike = reader.GetInt(3)
            */
            MySqlCommand command = SendCommand("SELECT * From PartModel");
            return command.ExecuteReader();
        }
        public MySqlDataReader GetPartStock()
        {
            /*IdPart = reader.GetInt(0)
             *NamePart = reader.GetInt(1)
             *IdPartColor = reader.GetInt(2)
             *NamePartColor = reader.GetString(3)
             *IdPartSize = reader.GetInt(4)
             *NamePartSize = reader.GetString(5)
             *NumberPartInStock =  reader.GetInt(6)
             *Threshold = reader.GetInt(7)
             *Location = reader.GetInt(8)
            */
            MySqlCommand command = SendCommand("SELECT IdPart, NamePart, IdPartColor, Color.NameColor, IdPartSize, Size.NameSize, NumberPartInStock, NumberPartOrdered, Threshold, Location From Part "+
                                               "INNER JOIN Color on Color.IdColor = IdPartColor " +
                                               "INNER JOIN Size on Size.IdSize = IdPartSize");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetNextIdOrder() {
            string commandToSend = "SELECT max(IdOrder) + 1 FROM OrderInfo";
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader GetNextIdOrderModel() {
            string commandToSend = "SELECT AUTO_INCREMENT FROM information_schema.Tables " +
                                   "WHERE TABLE_SCHEMA = \"GigaBike\" " +
                                   "AND TABLE_NAME = \"OrderModel\";";
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader AddOrderModel(int IdOrder, int IdModelBike) {
            string commandToSend = string.Format("INSERT INTO OrderModel (IdOrder, IdModelBike) VALUES ({0},{1});" +
                                                 "SELECT @@IDENTITY;", IdOrder, IdModelBike);
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader AddSeveralOrderModel(Order currentOrder) {
            string commandToSend = "INSERT INTO OrderModel(IdOrder, IdModelBike) VALUES";
            List<string> values = new List<string>();

            foreach (BikeOrder currentBikeOrder in currentOrder.Bikes)
                values.Add(string.Format("({0},{1})", currentOrder.IdOrder, currentBikeOrder.Bike.IdBike));

            commandToSend += string.Join(",", values) + ";SELECT @@IDENTITY;";

            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader SetCustomer(Customer customer) {
            string commandToSend = string.Format("INSERT INTO Customer (TVA, NameCustomer, AddressCustomer, PhoneCustomer) VALUES (\"{0}\", \"{1}\", \"{2}\",\"{3}\");",
                                                 customer.TVA, customer.Name, customer.Address, customer.Phone);
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader SaveCommand(Order order) {
            string commandToSend = string.Format("INSERT INTO OrderInfo (TVACustomer, Price, DeliveryDate) VALUES (\"{0}\",\"{1}\",\"{2}\");" +
                                                 "SELECT @@IDENTITY;", order.Customer.TVA, order.Price, order.DeliveryDate.ToString("yyyy-MM-dd"));
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader SaveCommandModels(int idOrder, Bike currentBike) {
            string commandToSend = string.Format("INSERT INTO OrderModel (IdOrder, IdModelBike) VALUES (\"{0}\",\"{1}\");" +
                                                 "SELECT @@IDENTITY;", idOrder, currentBike.IdBike);

            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader GetPlanning()
        {
            /*IdPlanning =  reader.GetInt(0)
              PlanningDate =  reader.GetDateTime(1)
              Slot  =  reader.GetInt(2)
              OrderModel  =  reader.GetInt(3)
              IsReady  =  reader.GetInt(4)
              IdOrder  =  reader.GetInt(5)
              IdModelBike  =  reader.GetInt(6)
            */
            MySqlCommand command = SendCommand("SELECT Planning.*, OrderModel.IdOrder, OrderModel.IdModelBike, Color.IdColor, Color.NameColor, Size.IdSize, Size.NameSize FROM Planning " +
                                                "INNER JOIN OrderModel ON OrderModel.IdOrderModel = Planning.OrderModel " +
                                                "INNER JOIN BikeModel ON BikeModel.IdModel = IdModelBike " +
                                                "INNER JOIN Color ON Color.IdColor = BikeModel.IdColor " +
                                                "INNER JOIN Size ON Size.IdSize = BikeModel.IdSize");
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
            
            MySqlCommand command = SendCommand("UPDATE Part set NumberPartInStock = NumberPartInStock + " + QuantityToAdd + " WHERE IdPart=" + IdPart);
            return command.ExecuteReader();
        }
        public MySqlDataReader DeletePartModelToStock(int IdPart, int QuantityToDelete)
        {
            //delete a quantity to Part.NumberPartInStock

            MySqlCommand command = SendCommand("UPDATE Part set NumberPartInStock = NumberPartInStock - " + QuantityToDelete + " WHERE IdPart=" + IdPart);
            return command.ExecuteReader();
        }

        public MySqlDataReader AddSlotToPlanning(Slot slot, int IdOrderModel) {
            string commandToSend = string.Format(string.Format("INSERT INTO Planning (PlanningDate, Slot, OrderModel) VALUES(\"{0}\",{1},{2});" +
                                                                "SELECT @@IDENTITY;", slot.Date.ToString("yyyy-MM-dd"), slot.SlotNumber, IdOrderModel));
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader AddSeveralSlotToPlanning(List<Slot> slotsToAdd) {
            string commandToSend = "INSERT INTO Planning (PlanningDate, Slot, OrderModel) VALUES";
            List<string> values = new List<string>();

            foreach (Slot slot in slotsToAdd) values.Add(string.Format("(\"{0}\",{1},{2})", slot.Date.ToString("yyyy-MM-dd"), slot.SlotNumber, slot.IdOrderModel));

            commandToSend += string.Join(",", values);
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }

        public MySqlDataReader DeleteSeveralSlotFromPlanning(List<Slot> slotToDelete) {
            string commandToSend = "DELETE FROM Planning WHERE (PlanningDate, Slot) IN ";
            List<string> values = new List<string>();

            foreach (Slot slot in slotToDelete) values.Add(string.Format("(\"{0}\", {1})", slot.Date.ToString("yyyy-MM-dd"), slot.SlotNumber));

            commandToSend += '(' + string.Join(",", values) + ')';

            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }
        public MySqlDataReader GetPurchaseOrder()
        {
            /*
             * IdPurchaseOrder = reader.GetInt(0)
             * PurchaseDate = reader.GetDateTime(1)
            */
            MySqlCommand command = SendCommand("SELECT * FROM PurchaseOrder");
            return command.ExecuteReader();
        }
        public MySqlDataReader AddPurchaseOrder(DateTime date)
        {   //add a time in the table PurchaseOrder
            MySqlCommand command = SendCommand(string.Format("INSERT INTO PurchaseOrder (PurchaseDate) VALUES(\"{0}\");" +
                                   "SELECT @@IDENTITY;", date.ToString("yyyy-MM-dd")));
            return command.ExecuteReader();
        }
        public MySqlDataReader GetPurchaseOrderPart()
        {
            /*
             * IdPurchaseOrderPart = reader.GetInt(0)
             * IdPurchaseOrder = reader.GetInt(1)
             * IdPart = reader.GetInt(2)
             * QuantityToOrder = reader.GetInt(3)
             * IsReceived = reader.GetInt(4)
             */
            string commandToSend = string.Format("SELECT * FROM PurchaseOrderPart");
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }
        public MySqlDataReader AddPurchaseOrderPart(int IdPurchaseOrder , int IdPart, int QuantityToOrder, bool IsReceived = false)
        {
            //insert a new element to the table PurchaseOrderPart
            string commandToSend = string.Format("INSERT INTO PurchaseOrderPart (IdPurchaseOrder, IdPart,QuantityToOrder,IsReceived ) VALUES ({0},{1},{2},{3});" +
                                                 "SELECT @@IDENTITY;", IdPurchaseOrder, IdPart,QuantityToOrder,IsReceived);
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }
        public MySqlDataReader SetReadyStatePurchaseOrderPart(int IdPurchaseOrder, bool isReceived)
        {
            //change the state of PurchaseOrderPart.IsReceived
            string commandToSend = string.Format("UPDATE PurchaseOrderPart SET IsReceived = " + isReceived + " where IdPurchaseOrderPart = " + IdPurchaseOrder);
            MySqlCommand command = SendCommand(commandToSend);
            return command.ExecuteReader();
        }
    }
}

