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

        public AddBikeStockPage() {
            InitializeComponent();
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
    }
}
