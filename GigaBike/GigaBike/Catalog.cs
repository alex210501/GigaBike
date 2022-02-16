using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Catalog {
        private List<Bike> bikes;

        public Catalog() {
            bikes = new List<Bike>();
        }

        public List<Bike> Bikes {
            get {
                return new List<Bike>(bikes);
            }
        }

        public void RefreshModels () { 
            // Refresh the models from the database
        }
    }
}
