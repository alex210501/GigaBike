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

namespace GigaBike {
    /// <summary>
    /// Interaction logic for ChoosePathPage.xaml
    /// </summary>
    public partial class ChoosePathPage : Page {
        Action goToCatalogCallback = null;
        Action goToRessourcesCallback = null;

        public ChoosePathPage() {
            InitializeComponent();
        }
        private void ButtonBackToLogin(object sender, RoutedEventArgs e) {
        }

        private void ButtonToRessource(object sender, RoutedEventArgs e) {
            if (goToRessourcesCallback is not null) goToRessourcesCallback();
        }

        private void ButtonToCatalog(object sender, RoutedEventArgs e) {
            if (goToCatalogCallback is not null) goToCatalogCallback();
        }

        public Action GoToCatalogCallback {
            set {
                goToCatalogCallback = value;
            }
        }

        public Action GoToRessourcesCallback {
            set {
                goToRessourcesCallback = value;
            }
        }
    }
}
