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
    public partial class BikeModelWindow : Window {
        public BikeModelWindow() {
            InitializeComponent();
            //Here put command DB size
            //For element in DB LIST (BikeSize.Items.Add("element");
            BikeSize.Items.Add("26");
            BikeSize.Items.Add("28");
            //Here put command DB color
            //For element in DB LIST (BikeColor.Items.Add("element");
            BikeColor.Items.Add("Blue");
            BikeColor.Items.Add("Red");
            BikeColor.Items.Add("Green");

        }

        private void ButtonBackToCatalog(object sender, RoutedEventArgs e) {
            CatalogWindow cat = new CatalogWindow();
            cat.Show();
            this.Hide();
        }

        private void ListSize1(object sender, RoutedEventArgs e) {
        }

        private void ListSize2(object sender, RoutedEventArgs e) {
        }

        private void ListColor_All(object sender, SelectionChangedEventArgs e) {
        }

        private void ListSize_All(object sender, SelectionChangedEventArgs e) {

        }

        private void Text_Input_Quantity(object sender, TextChangedEventArgs e) {
        }

        private void ButtonCommandNext(object sender, RoutedEventArgs e) {
            OrderValidationWindow OV1 = new OrderValidationWindow();
            OV1.Show();
            this.Hide();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e) {
        }
    }

    public class TodoItem {
        public string Color { get; set; }
    }
}
