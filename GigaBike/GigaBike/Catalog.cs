using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class Catalog {
        private List<Bike> bikes;

        Catalog() {
            bikes = new List<Bike>();
        }

        List<Bike> Bikes {
            get {
                return new List<Bike>(bikes);
            }
        }

        void RefreshModels () { 
            // Refresh the models from the database
        }
    }
}
