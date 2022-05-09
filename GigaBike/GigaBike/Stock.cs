using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace GigaBike
{
    public class Stock {
        public int IdOrder { get; set; }
        private List<Part> pieceList;
        private DataBase database;
        public PartToModelLinker PartToModelLinker { get; }
        public PurchaseOrderPartHandler PurchaseOrderPartHandler { get; }


        public Stock(DataBase database) {
            pieceList = new List<Part>();
            PartToModelLinker = new PartToModelLinker();
            PurchaseOrderPartHandler = new PurchaseOrderPartHandler(database);
            this.database = database;
        }

        public List<Part> Parts {
            get {
                return new List<Part>(pieceList);
            }
        }

        public void GetStockFromDataBase() {
            // Clear the pieces list
            pieceList.Clear();

            MySqlDataReader reader = database.GetPartStock();

            while (reader.Read()) {
                int idPart = reader.GetInt32(0);
                string namePart = reader.GetString(1);
                int idColor = reader.GetInt32(2);
                string nameColor = reader.GetString(3);
                int idSize = reader.GetInt32(4);
                string nameSize = reader.GetString(5);
                int numberPart = reader.GetInt32(6);
                int quantityOrdered = reader.GetInt32(7);
                int threshold = reader.GetInt32(8);
                int location = reader.GetInt32(9);

                Part currentPieceStock = new Part(idPart, namePart, numberPart, quantityOrdered, threshold, location, new Color(idColor, nameColor), new Size(idSize, nameSize));
                pieceList.Add(currentPieceStock);
            }
            reader.Close();

        }

        public void GetPartPerBikeFromDatabase() {
            MySqlDataReader reader = database.GetPartModel();

            PartToModelLinker.ClearParts();
            while (reader.Read()) {
                int idPartModel = reader.GetInt32(0);
                int idPart = reader.GetInt32(1);
                int idModel = reader.GetInt32(2);
                int numberForBike = reader.GetInt32(3);

                Part currentPart = pieceList.Find(part => part.IdPart == idPart);

                if (currentPart is null) throw new Exception("The part has not been found in the Stock !");

                BikePart currentBikePart = new BikePart(currentPart, numberForBike);
                PartToModelLinker.AddPartForIdModel(idModel, currentBikePart);
            }

            reader.Close();
        }

        public bool IsThereEnoughPartsInStock(int NumberPart, int NumberPartCommand) {
            return NumberPart >= NumberPartCommand;// check if there are enougth part for the command
        }

        public int GetQuantityInStockForPart(Part part) {
            Part partSearched = pieceList.Find(p => p.IdPart == part.IdPart);

            if (partSearched is null)
                throw new Exception("Part not found in stock !");

            return part.QuantityInStock;
        }

        public void RefreshPurchaseOrderFromDataBase() {
            PurchaseOrderPartHandler.GetPurchaseFromDataBase();
            GetPurchaseOrderPartFromDataBase();
        }

        private void GetPurchaseOrderPartFromDataBase() {
            MySqlDataReader reader = database.GetPurchaseOrderPart();

            while (reader.Read()) {
                int idPurchaseOrderPart = reader.GetInt32(0);
                int idPurchaseOrder = reader.GetInt32(1);
                int idPart = reader.GetInt32(2);
                int quantityToOrder = reader.GetInt32(3);
                // IsReceived = reader.GetInt(4)

                Part purchasePart = pieceList.Find(p => p.IdPart == idPart);

                if (purchasePart is null)
                    throw new Exception("Part is not found !");

                //TODO: Change the class name
                PurchaseOrderPartHandler.AddPartToPurchaseOrderById(idPurchaseOrder, purchasePart, quantityToOrder);
            }

            reader.Close();
        }
    }
    public class BikeInStock
    {
        private List<StockBike> stockBikes;
        private DataBase database;
        public BikeInStock(DataBase database)
        {
            stockBikes = new List<StockBike>();
            this.database = database;
        }

        public List<StockBike> getBikeStock
        {
            get
            {
                MySqlDataReader reader = database.GetBikeStock();
                stockBikes.Clear();

                while (reader.Read())
                {   //read all from GetBikeStock method
                    int IdModel = reader.GetInt32(0);
                    int IdBike = reader.GetInt32(1);
                    int idColor = reader.GetInt32(2);
                    int idSize = reader.GetInt32(3);
                    int price = reader.GetInt32(4);
                    string imagePath = reader.GetString(5);
                    int slotDuration = reader.GetInt32(6);
                    int quantity = reader.GetInt32(7);
                    string nameColor = reader.GetString(8);
                    string nameSize = reader.GetString(9);
                    string nameBike = reader.GetString(10);

                    
                    Bike bike = new Bike(IdBike, nameBike, price, new Color(idColor, nameColor), new Size(idSize, nameSize), imagePath, slotDuration);
                    StockBike currentBikeStock = new StockBike(bike, IdModel,quantity);//create a bike before to create a StockBike
                    stockBikes.Add(currentBikeStock);
                }

                reader.Close();
                return stockBikes;
            }
        }
    }
}
