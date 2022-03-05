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
    /// Logique d'interaction pour OrderListPage.xaml
    /// </summary>
    public partial class PM_OrderListPage : Page
    {
        private Action goToPlanningCallback = null;
        private Action goBackToRessourceCallback = null;

        public PM_OrderListPage() {
            InitializeComponent();
        }

        private void ButtonGoToPlanning(object sender, RoutedEventArgs e) {
            if (goToPlanningCallback is not null) goToPlanningCallback();
        }

        private void ButtonGoBackToRessource(object sender, RoutedEventArgs e) {
            if (goBackToRessourceCallback is not null) goBackToRessourceCallback();
        }

        private void DataGridOrderList(object sender, SelectionChangedEventArgs e) {

        }

        public Action GoToPlanningCallback {
            set {
                goToPlanningCallback = value;
            }
        }

        public Action GoBackToRessourceCallback {
            set {
                goBackToRessourceCallback = value;
            }
        }
    }
}
