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
    /// Logique d'interaction pour RessourcesPage.xaml
    /// </summary>
    public partial class PM_RessourcesPage : Page
    {
        private Action goToOrderListCallback = null;
        private Action goToStockCallback = null;
        private Action goToChoosePathCallback = null;

        public PM_RessourcesPage() {
            InitializeComponent();
        }

        private void ButtonOrderList(object sender, RoutedEventArgs e) {
            if (goToOrderListCallback is not null) goToOrderListCallback();
        }

        private void ButtonStock(object sender, RoutedEventArgs e) {
            if (goToStockCallback is not null) goToStockCallback();
        }

        private void ButtonGobackToChoosePath(object sender, RoutedEventArgs e) {
            if (goToChoosePathCallback is not null) goToChoosePathCallback();
        }

        public Action GoToOrderListCallback {
            set {
                goToOrderListCallback = value;
            }
        }

        public Action GoToStockCallback {
            set {
                goToStockCallback = value;
            }
        }

        public Action GoToChoosePathCallback {
            set {
                goToChoosePathCallback = value;
            }
        }
    }
}
