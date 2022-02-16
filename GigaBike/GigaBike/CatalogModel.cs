using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class CatalogModel {
        public int IdModel { get; }
        public string Name { get; }
        public int Price { get; private set; }
        private List<Bike> bikes;

        CatalogModel(int IdModel) {
            this.IdModel = IdModel;
            bikes = new List<Bike>();
        }

        void AddBike(Bike bike) {
            if (IsBikeRegistered(bike) == false)
                bikes.Add(bike);
            Price = bike.Price;
        }

        bool IsBikeRegistered(Bike bike) {
            foreach(Bike bikeRegistered in bikes) {
                if (bikeRegistered.IdBike == bike.IdBike) 
                    return true;
            }

            return false;
        }

        public List<Color> Color {
            get {
                HashSet<Color> colors = new HashSet<Color>();

                foreach (Bike bike in bikes) colors.Add(bike.Color);

                return new List<Color>(colors.ToList());
            }
        }

        public List<Size> Size{
            get {
                HashSet<Size> sizes = new HashSet<Size>();

                foreach (Bike bike in bikes) sizes.Add(bike.Size);

                return new List<Size>(sizes.ToList());
            }
        }
    }
}
