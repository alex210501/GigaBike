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
        private List<PartModel> pieceList;
        public Customer Customer { get; private set; }
        private DataBase database;
        public DateTime DateDelivery { get; }
        public int Duration { get; set; }

        public Order(DataBase database) {
            bikes = new List<BikeOrder>();
            Customer = new Customer();
            pieceList = new List<PartModel>();
            this.database = database;
        }

        public void Save(Customer customer) {
            Customer = new Customer(customer);

            SaveCutomer(customer);
        }
        // Changement validate pour rajouter la liaison des pièces aux vélos
        public void Validate() {
            MySqlDataReader reader = database.SaveCommand(this);
            reader.Read();
            IdOrder = reader.GetInt32(0);
            reader.Close();
            MySqlDataReader reader2 = database.GetStock();

            while (reader2.Read())
            {
                int IdPart = reader2.GetInt32(1);
                int IdModelBike = reader2.GetInt32(2);
                int NumberForBike = reader2.GetInt32(3);
                int NumberPart = reader2.GetInt32(8);
                int Threshold = reader2.GetInt32(9);

                PartModel currentPieceStock = new PartModel(IdPart, IdModelBike, NumberForBike, NumberPart, Threshold);
                pieceList.Add(currentPieceStock);
            }
            reader.Close();

            foreach (BikeOrder currentBikeOrder in bikes) {
                MySqlDataReader bikeOrderReader = database.SaveCommandModels(IdOrder, currentBikeOrder);
                bikeOrderReader.Close();
                foreach (PartModel currentPiece in pieceList)
                {
                    if(currentBikeOrder.Bike.IdBike == currentPiece.IdModelBike) //PartModel.AreComponant(
                    {
                        if (currentPiece.NumberPart >= currentPiece.NumberForBike) //PartModel.ArePartingStockSufficient
                        {
                            //int numberPart = currentPiece.NumberPart - currentPiece.NumberForBike;
                            //MySqlDataReader pieceLinkerReader = database.SaveChangeNumberPart(currentPiece.IdPart, numberPart);
                            //pieceLinkerReader.Close();
                        }
                        else
                        {
                            //order
                        }
                    }
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
        //rajout list PartModel
        public List<PartModel> PartModels
        {
            get
            {
                return new List<PartModel>(pieceList);
            }
        }
    }
}
