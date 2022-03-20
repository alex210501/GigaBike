using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GigaBike {
    class PartToModelLinker {
        private Dictionary<int, List<BikePart>> partsPerBike;

        public PartToModelLinker() {
            partsPerBike = new Dictionary<int, List<BikePart>>();
        }

        public List<BikePart> GetPartsForIdModel(int idModel) {
            if (partsPerBike.ContainsKey(idModel))
                return new List<BikePart>(partsPerBike[idModel]);
            return new List<BikePart>();
        }

        public void AddPartForIdModel(int idModel, BikePart part) {
            // If the dictionnary doesn't contain the key, create an empty list
            if (!partsPerBike.ContainsKey(idModel))
                partsPerBike.Add(idModel, new List<BikePart>());
            partsPerBike[idModel].Add(part);
        }

        public void ClearParts() {
            // Remove all the dictionnary items
            partsPerBike.Clear();
        }

        public void ClearPartsForIdModel(int idModel) {
            // Clear the part for only a bike model
            if (partsPerBike.ContainsKey(idModel))
                partsPerBike[idModel].Clear();
        }
    }
}
