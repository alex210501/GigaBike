using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class Login {
        private string Username { get; set; }
        private string Password { get; set; }

        public bool CheckUser(string username, string password) {
            // Faire une requête en DB ici pour obtenir le mot de passe
            // et vérifier sa correspondance avec le username
            // Si le username n'est pas en DB, renvoie false
            return true;
        }
    }
}
