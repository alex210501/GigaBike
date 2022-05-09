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
    /// Logique d'interaction pour BikeStock.xaml
    /// </summary>
    public partial class BikeStockPage : Page {
        private Action goBackToStockCallback = null;
        private Action goToAddBikeToStockCallback = null;

        public BikeStockPage(List<StockBike> AllBikesInStock) {
            InitializeComponent();
            BikeStockList(AllBikesInStock);
        }

        public Action GoBackToStockCallback {

            set {
                goBackToStockCallback = value;
                
            }
        }

        public Action GoToAddBikeToStockCallback {
            set {
                goToAddBikeToStockCallback = value;
            }
        }

        private void DataGridBikeStock(object sender, SelectionChangedEventArgs e) {

        }

        private void ButtonGoBackToStock(object sender, RoutedEventArgs e) {
            if (goBackToStockCallback is not null) goBackToStockCallback();
        }

        private void AddBikeToStockButton(object sender, RoutedEventArgs e) {
            if (goToAddBikeToStockCallback is not null) goToAddBikeToStockCallback();
        }
        public void BikeStockList(List<StockBike> AllBikesInStock)
        {
            List<StockBikeGrid> stockRecap = new List<StockBikeGrid>();
            foreach (StockBike bike in AllBikesInStock)
            {
                StockBikeGrid currentStockRecapGrid = new StockBikeGrid();

                currentStockRecapGrid.Name = bike.Bike.Name;
                currentStockRecapGrid.QuantityInStock = bike.Quantity;
                currentStockRecapGrid.Color = bike.Bike.Color;
                currentStockRecapGrid.Size = bike.Bike.Size;
                currentStockRecapGrid.Price = bike.Bike.Price;
                //add bikes rows to currentStockRecapGrid
                stockRecap.Add(currentStockRecapGrid);
            }
            DataGridBikeStock_.ItemsSource = stockRecap;//add bikes to dataGrid
            AllBikesInStock.Clear();
        }
        public class StockBikeGrid
        {
            public string Name { get; set; }
            public int QuantityInStock { get; set; }
            public Color Color { get; set; }
            public Size Size { get; set; }
            public int Price { get; set; }

        }
    }
}
