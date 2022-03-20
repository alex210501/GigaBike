using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    class PartToModelLinker {
        private Dictionary<int, List<Part>> partsPerBike;
        public PartToModelLinker() {
            partsPerBike = new Dictionary<int, List<Part>>();
        }

        public List<Part> GetPartsForIdOrderModel(int idOrderModel) {
            if (partsPerBike.ContainsKey(idOrderModel))
                return new List<Part>(partsPerBike[idOrderModel]);
            return new List<Part>();
        }

        public void AddPartForIdOrderModel(int idOrderModel, Part part) {
            // If the dictionnary doesn't contain the key, create an empty list
            if (!partsPerBike.ContainsKey(idOrderModel))
                partsPerBike.Add(idOrderModel, new List<Part>());
            partsPerBike[idOrderModel].Add(part);
        }

        public void ClearParts() {
            // Remove all the dictionnary items
            partsPerBike.Clear();
        }

        public void ClearPartsForIdOrderModel(int idOrderModel) {
            // Clear the part for only a bike model
            if (partsPerBike.ContainsKey(idOrderModel))
                partsPerBike[idOrderModel].Clear();
        }
    }
}
