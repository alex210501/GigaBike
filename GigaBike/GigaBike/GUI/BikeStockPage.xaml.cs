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

        public BikeStockPage() {
            InitializeComponent();
        }

        private void DataGridBikeStock(object sender, SelectionChangedEventArgs e) {

        }

        private void ButtonGoBackToStock(object sender, RoutedEventArgs e) {
            if (goBackToStockCallback is not null) goBackToStockCallback();
        }

        public Action GoBackToStockCallback {
            set {
                goBackToStockCallback = value;
            }
        }
    }
}
