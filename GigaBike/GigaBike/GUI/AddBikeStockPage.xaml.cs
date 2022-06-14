using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GigaBike
{
    /// <summary>
    /// Logique d'interaction pour OrderBikePage.xaml
    /// </summary>
    public partial class AddBikeStockPage : Page {
        private Action addBikeToStockCallback = null;
        private Action goBackToChooseCallback = null;
        private List<StockBike> AllBikesInStock;

        public AddBikeStockPage(List<StockBike> AllBikesInStock) {
            
            InitializeComponent();
            this.AllBikesInStock = AllBikesInStock.OrderBy(o => o.IdModel).ToList(); //order bikes in stock by their Id
            SelectBikeInStock(AllBikesInStock);
        }

        public Action AddBikeToStockCallback {
            set {
                addBikeToStockCallback = value;
            }
        }

        public Action GoBackToChooseCallback {
            set {
                goBackToChooseCallback = value;
            }
        }

        private void ButtonAddBikeToStock(object sender, RoutedEventArgs e) {
            if (addBikeToStockCallback is not null) addBikeToStockCallback();
        }
       

        private void ButtonBackToChoose(object sender, RoutedEventArgs e) {
            if (goBackToChooseCallback is not null) goBackToChooseCallback();
        }

        public void SelectBikeInStock(List<StockBike> AllBikesInStock) {
            AllBikesInStock = AllBikesInStock.OrderBy(o => o.Bike.Name).ToList(); //order bikes by their names

            // Add bike to the ComboBox
            foreach (StockBike bike in AllBikesInStock)
            {
                AddBikeComboboxItem newItem = new AddBikeComboboxItem();
                
                // Assign Bike items
                newItem.ValueToDisp = bike.Bike.Name + " | " + bike.Bike.Color.Name + " | " + bike.Bike.Size.Name;
                newItem.BikeModelID = bike.IdModel;
                newItem.BikeName = bike.Bike.Name;
                newItem.BikeColor = bike.Bike.Color;
                newItem.BikeSize= bike.Bike.Size;

                // Add Bike to ComboBox
                BikeModel.Items.Add(newItem);
            }
            AllBikesInStock.Clear();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        public int GetModelToAdd() {
            AddBikeComboboxItem model = BikeModel.SelectedItem as AddBikeComboboxItem;
            if (model is not null)
                return model.BikeModelID;

            return 0;
        }

        public int GetQuantityToAdd() {
            return short.Parse(QuantityTextBox_.Text);//get the number of quantity input
        }
    }

    class AddBikeComboboxItem {
        public string ValueToDisp { get; set; }
        public int BikeModelID { get; set; }
        public string BikeName { get; set; }
        public Color BikeColor { get; set; }
        public Size BikeSize { get; set; }
    }
}
