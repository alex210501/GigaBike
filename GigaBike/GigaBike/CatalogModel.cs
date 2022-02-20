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

        public List<Color> AvailableColor {
            get {
                Dictionary<int, Color> colors = new Dictionary<int, Color>();

                foreach (Bike bike in bikes) colors[bike.Color.IdColor] = bike.Color;

                return new List<Color>(colors.Values);
            }
        }

        public List<Size> AvailableSize{
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

        public Bike GetBike(Color color, Size size) {
            foreach (Bike bike in bikes) {
                if ((bike.Color.IdColor == color.IdColor) && (bike.Size.IdSize == size.IdSize))
                    return bike;
            }

            throw new BikeNotFoundException(string.Format("The bike with the color {0} and the size {1} is not found !", color.Name, size.Name));
        }

        public int GetPrice(Color color, Size size) {
            Bike bike = GetBike(color, size);
            return bike.Price;
        }

        public Bike GetFirstBike() {
            return bikes[0];
        }
    }
}
