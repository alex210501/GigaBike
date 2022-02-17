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
using System.Windows.Shapes;

namespace GigaBike {
    /// <summary>
    /// Interaction logic for ChoosePathWindow.xaml
    /// </summary>
    public partial class ChoosePathWindow : Window {
        Action goToCatalogCallback = null;
        public ChoosePathWindow() {
            InitializeComponent();
        }
        private void ButtonBackToLogin(object sender, RoutedEventArgs e) {
            /* LoginWindow log = new LoginWindow();
            log.Show();
            this.Hide(); */
        }

        private void ButtonToRessource(object sender, RoutedEventArgs e) {

        }

        private void ButtonToCatalog(object sender, RoutedEventArgs e) {
            if (goToCatalogCallback is not null) goToCatalogCallback();
        }

        public Action GoToCatalogCallback {
            set {
                goToCatalogCallback = value;
            }
        }
    }
}
