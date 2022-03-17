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

            while (reader.Read())
            {
                int IdPart = reader.GetInt32(0);
                string NamePart = reader.GetString(1);
                int NumberPart = reader.GetInt32(4);
                int Threshold = reader.GetInt32(5);
                int Location = reader.GetInt32(6);

                Part currentPieceStock = new Part(IdPart, NamePart, NumberPart, Threshold, Location);
                pieceList.Add(currentPieceStock);
            }
            reader.Close();
        }
        public bool IsThereEnoughPartsInStock(int NumberPart, int NumberPartCommand) //NumberPartCommand n'existe pas se sera le nombre de pièces besoin pour la commande
        {
            return NumberPart >= NumberPartCommand;// check if there are enougth part for the command
        }


    }
}
