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
    /// Logique d'interaction pour StockPage.xaml
    /// </summary>
    public partial class StockPage : Page
    {
        private Action goToBikeStockCallback = null;
        private Action goToPiecesStockCallback = null;
        private Action goBackToRessourcesCallback = null;

        public StockPage() {
            InitializeComponent();
        }

        private void ButtonBikeStock(object sender, RoutedEventArgs e) {
            if (goToBikeStockCallback is not null) goToBikeStockCallback();
        }

        private void ButtonPiecesStock(object sender, RoutedEventArgs e ) {
            if (goToPiecesStockCallback is not null) goToPiecesStockCallback();
        }

        private void ButtonGobackToRessource(object sender, RoutedEventArgs e) {
            if (goBackToRessourcesCallback is not null) goBackToRessourcesCallback();
        }

        public Action GoToBikeStockCallback {
            set {
                goToBikeStockCallback = value;
            }
        }

        public Action GoToPiecesStockCallback {
            set {
                goToPiecesStockCallback = value;
            }
        }

        public Action GoBackToRessourcesCallback {
            set {
                goBackToRessourcesCallback = value;
            }
        }
    }
}
