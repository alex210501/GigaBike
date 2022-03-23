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
    /// Logique d'interaction pour OrderPartPage.xaml
    /// </summary>
    public partial class OrderPartPage : Page
    {
        private Action createPurchaseCallback = null;
        private Action orderPartCallback = null;
        private Action buttonBackCallback = null;

        public OrderPartPage()
        {
            InitializeComponent();
        }

        public Action CreatePurchaseCallback {
            set {
                createPurchaseCallback = value;
            }
        }

        public Action OrderPartCallback {
            set {
                orderPartCallback = value;
            }
        }

        public Action ButtonBackCallback {
            set {
                buttonBackCallback = value;
            }
        }

        private void ButtonCreateNewPurchases(object sender, RoutedEventArgs e) {
            if (createPurchaseCallback is not null) createPurchaseCallback();
        }

        private void ButtonOrderPart(object sender, RoutedEventArgs e) {
            if (orderPartCallback is not null) orderPartCallback();
        }

        private void ButtonBack(object sender, RoutedEventArgs e) {
            if (buttonBackCallback is not null) buttonBackCallback();
        }
    }
}
