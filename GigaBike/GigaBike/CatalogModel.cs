using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GigaBike {
    public class CatalogModel {
        public int IdModel { get; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        private List<Bike> bikes;

        public CatalogModel(int IdModel) {
            this.IdModel = IdModel;
            bikes = new List<Bike>();
        }

        public void AddBike(Bike bike) {
            if (IsBikeRegistered(bike) == false)
                bikes.Add(bike);
            Name = bike.Name;
            Price = bike.Price;
        }

        public List<Color> Color {
            get {
                Dictionary<int, Color> colors = new Dictionary<int, Color>();

                foreach (Bike bike in bikes) colors[bike.Color.IdColor] = bike.Color;

                return new List<Color>(colors.Values);
            }
        }

        public List<Size> Size{
            get {
                Dictionary<int, Size> sizes = new Dictionary<int, Size>();

                foreach (Bike bike in bikes) sizes[bike.Size.IdSize] = bike.Size;

                return new List<Size>(sizes.Values);
            }
        }

        bool IsBikeRegistered(Bike bike) {
            foreach (Bike bikeRegistered in bikes) {
                if (bikeRegistered.IdBike == bike.IdBike)
                    return true;
            }

            return false;
        }
    }
}
