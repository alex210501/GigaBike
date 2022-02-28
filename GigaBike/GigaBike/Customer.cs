using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Customer {
        public string Name { get; set; }
        public string Address { get; set; }
        public string TVA { get; set; }
        public string Phone { get; set; }

        public Customer() { }

        public Customer(Customer otherCustomer) {
            this.Name = otherCustomer.Name;
            this.Address = otherCustomer.Address;
            this.TVA = otherCustomer.TVA;
            this.Phone = otherCustomer.Phone;
        }
    }
}
