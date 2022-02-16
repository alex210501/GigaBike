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
    public partial class CatalogWindow : Window {
        public CatalogWindow() {
            InitializeComponent();
            NameBike.Text = "City Bike";
        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e) {

        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e) {

        }

        private void ButtonBackToChoose(object sender, RoutedEventArgs e) {
            /* ChoosePath path = new ChoosePath();
            path.Show();
            this.Hide(); */
        }

        private void ButtonCheck1(object sender, RoutedEventArgs e) {
            /*BikeModelWindow cb1 = new BikeModelWindow(); // a changer lorsque l'on va appeler la base ==> création de form automatique avec les informations => besoin création template
            cb1.Show();
            this.Hide();*/
        }
    }
}
