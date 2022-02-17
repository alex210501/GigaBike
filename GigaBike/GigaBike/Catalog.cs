using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Catalog {
        private int currentModel = 0;
        private int numberModels = 0;
        private List<CatalogModel> models;

        public Catalog() {
            models = new List<CatalogModel>();
        }

        public List<CatalogModel> Models {
            get {
                return new List<CatalogModel>(models);
            }
        }

        public void RefreshModels () {
            // Refresh the models from the database

            // Test: Create model while waiting the database management
            // Clear the model list to create a new one
            models.Clear();

            Bike bike1 = new Bike(1, "Explorer", 100, new Color(1, "Green"), new Size(1, "28''"));
            Bike bike2 = new Bike(2, "Explorer", 100, new Color(2, "Red"), new Size(1, "28''"));
            Bike bike3 = new Bike(3, "Adventure", 100, new Color(2, "Red"), new Size(1, "26''"));

            CatalogModel catalogModel1 = new CatalogModel(1);
            CatalogModel catalogModel2 = new CatalogModel(2);

            catalogModel1.AddBike(bike1);
            catalogModel1.AddBike(bike2);
            catalogModel2.AddBike(bike3);

            models.Add(catalogModel1);
            models.Add(catalogModel2);

            numberModels = models.Count;
        }

        public CatalogModel GetCurrentModel() {
            return models[currentModel];
        }

        public void NextModel() {
            if (currentModel < (numberModels - 1)) currentModel++;
        }

        public void PreviousModel() {
            if (currentModel > 0) currentModel--;
        }
    }
}
