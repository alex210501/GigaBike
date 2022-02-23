using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class Planning {
        private List<Week> weeks;

        public Planning() {
            weeks = new List<Week>();
        }

        public void Refresh() {
            // Get information from the databse
        }

        public DateTime GetDeliveryDate(BikeOrder bikeOrder) {
            return new DateTime();
        }

        public List<Slot> GetSlots(int duration) {
            return new List<Slot>();
        }

        public List<Week> Weeks {
            get {
                return new List<Week>(weeks);
            }
        }
    }
}
