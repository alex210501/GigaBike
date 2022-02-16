using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Controller {
        public Login Login { get; }
        public Catalog Catalog { get; }
        public Order Order { get; }
        public DataBase DataBase { get; }

        public Controller() {
            this.Login = new Login();
            this.Catalog = new Catalog();
            this.Order = new Order();
            this.DataBase = new DataBase();
        }

        public void Init() {
            // Make the init sequence
        }

        public void AddToOrder(Bike bike, int quantity) {
            // Add to order sequence
        }
    }
}
