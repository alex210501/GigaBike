using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace GigaBike {
    public class Catalog {
        private int currentModel = 0;
        private int numberModels = 0;
        private List<CatalogModel> models;
        private DataBase database = null;

        public Catalog(DataBase database) {
            this.database = database;
            models = new List<CatalogModel>();
        }

        public List<CatalogModel> Models {
            get {
                return new List<CatalogModel>(models);
            }
        }

        public void RefreshModels () {
            // Clear the model list to create a new one
            models.Clear();

            MySqlDataReader reader = database.GetModels();

            while (reader.Read()) {
                int idModel = reader.GetInt32(0);
                int idBike = reader.GetInt32(1);
                string bikeName = reader.GetString(2);
                int idColor = reader.GetInt32(3);
                string nameColor = reader.GetString(4);
                int idSize = reader.GetInt32(5);
                string nameSize = reader.GetString(6);
                int priceBike = reader.GetInt32(7);
                string imagePath = reader.GetString(8);

                Bike currentBike = new Bike(idModel, bikeName, priceBike, new Color(idColor, nameColor), new Size(idSize, nameSize), imagePath);
                AddBike(idBike, currentBike);

                Trace.WriteLine("---New Trace---");
                foreach (CatalogModel model in models) Trace.WriteLine(model.IdModel);
            }

            reader.Close();

            /* Bike bike1 = new Bike(1, "Explorer", 100, new Color(1, "Green"), new Size(1, "28''"), "Explorer.png");
            Bike bike2 = new Bike(2, "Explorer", 100, new Color(2, "Red"), new Size(1, "28''"), "Explorer.png");
            Bike bike3 = new Bike(3, "Adventure", 100, new Color(2, "Red"), new Size(1, "26''"), "Adventure.jpg");
            Bike bike4 = new Bike(4, "City", 100, new Color(1, "Green"), new Size(1, "28''"), "City.jpg");

            CatalogModel catalogModel1 = new CatalogModel(1);
            CatalogModel catalogModel2 = new CatalogModel(2);
            CatalogModel catalogModel3 = new CatalogModel(4);

            catalogModel1.AddBike(bike1);
            catalogModel1.AddBike(bike2);
            catalogModel2.AddBike(bike3);
            catalogModel3.AddBike(bike4);

            models.Add(catalogModel1);
            models.Add(catalogModel2);
            models.Add(catalogModel3);*/

            numberModels = models.Count;
        }

        public void AddBike(int idModel, Bike bike) {
            CatalogModel currentModel = GetCatalogModelByIdModel(idModel);

            if (currentModel is null) {
                currentModel = new CatalogModel(idModel);
                models.Add(currentModel);
            }

            currentModel.AddBike(bike);
        }

        public CatalogModel GetCatalogModelByIdModel(int idModel) {
            foreach(CatalogModel currentModel in models) {
                if (currentModel.IdModel == idModel)
                    return currentModel;
            }

            return null;
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

        public Bike GetSelectedBike(Color colorBike, Size sizeBike) {
            CatalogModel catalogModel = GetCurrentModel();

            return catalogModel.GetBike(colorBike, sizeBike);
        }
    }
}
