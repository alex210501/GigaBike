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
            this.connection = new MySqlConnection(string.Format(@"server={0};userid={1};pwd={2};persistsecurityinfo=True;database=dvauto;port={3};sharedmemoryname=", host, username, password, port));
        }

        public MySqlDataReader GetPassword(string username) {
            // Tape la commande pour le mot de passe ici à l'intérieur
            MySqlCommand command = SendCommand("") ;
            return command.ExecuteReader();
        }

        public MySqlDataReader GetUniqueModel(int modelId) {
            MySqlCommand command = SendCommand("");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetModels() {
            MySqlCommand command = SendCommand("");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetOrders() {
            MySqlCommand command = SendCommand("");
            return command.ExecuteReader();
        }

        public MySqlDataReader GetCustomer(int customerId) {
            MySqlCommand command = SendCommand("");
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

