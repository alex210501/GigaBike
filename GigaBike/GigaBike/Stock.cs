using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace GigaBike
{
    public class Stock
    {
        public int IdOrder { get; set; }
        private List<Part> pieceList;
        private DataBase database;
        public Stock(DataBase database)
        {
            pieceList = new List<Part>();
            this.database = database;
        }

        public List<Part> Parts
        {
            get
            {
                return new List<Part>(pieceList);
            }
        }
        
        public void GetStockFromDataBase()
        {
            MySqlDataReader reader = database.GetPartStock();

            while (reader.Read()) {
                int idPart = reader.GetInt32(0);
                string namePart = reader.GetString(1);
                int idColor = reader.GetInt32(2);
                string nameColor = reader.GetString(3);
                int idSize = reader.GetInt32(4);
                string nameSize = reader.GetString(5);
                int numberPart = reader.GetInt32(6);
                int threshold = reader.GetInt32(7);
                int location = reader.GetInt32(8);

                Part currentPieceStock = new Part(idPart, namePart, numberPart, threshold, location, new Color(idColor, nameColor), new Size(idSize, nameSize));
                pieceList.Add(currentPieceStock);
            }
            reader.Close();
        }

        public bool IsThereEnoughPartsInStock(int NumberPart, int NumberPartCommand) {
            return NumberPart >= NumberPartCommand;// check if there are enougth part for the command
        }


    }
}
