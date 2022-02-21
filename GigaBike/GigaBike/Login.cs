using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GigaBike {
    public class Login {
        private string Username { get; set; }
        private string Password { get; set; }
        private DataBase database = null;

        public Login(DataBase database)
        {
            this.database = database;
        }

        public bool CheckUser(string username, string password) {            
            try
            {
                MySqlDataReader reader = database.GetPassword(username);
                reader.Read();
                string passwordDatabase = reader.GetString(0);
                reader.Close();
                return password == passwordDatabase;
            }
            catch (Exception e)
            {
                return false;
            }

            
            
            




        }
    }
}
