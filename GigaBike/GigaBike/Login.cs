using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;

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
            bool isGoodPassword = false;


            try {
                MySqlDataReader reader = database.GetPasswordAndRole(username);
                if (reader.Read())
                    isGoodPassword = (password == reader.GetString(0));
                reader.Close();
            }
            catch (Exception) {
                isGoodPassword = false;
            }

            return isGoodPassword;
        }
    }
}
