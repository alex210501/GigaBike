using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class BikeNotFoundException : Exception {
        public BikeNotFoundException() {
        }

        public BikeNotFoundException(string message)
            : base(message) {
        }

        public BikeNotFoundException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}
