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

        public BikeStockPage() {
            InitializeComponent();
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
    }
}
