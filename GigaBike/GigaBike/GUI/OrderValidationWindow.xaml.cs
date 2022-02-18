using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GigaBike {
    public partial class OrderValidationWindow : Window {
        private Action backToCatalogWindow = null;

        public OrderValidationWindow(CatalogModel currentModel) {
            InitializeComponent();
            Text_Box_Estimation_Date.Text = "Estimation Date :";
        }

        private void ButtonBackToModels_Click(object sender, RoutedEventArgs e) {
            
        }

        private void Text_Input_Name(object sender, TextChangedEventArgs e) {

        }

        private void Text_Input_Adress(object sender, TextChangedEventArgs e) {

        }
        private void Text_Input_TVA(object sender, TextChangedEventArgs e) {

        }
        private void Text_Input_Phone_Number(object sender, TextChangedEventArgs e) {

        }

        private void ButtonSavePurchase(object sender, RoutedEventArgs e) {

        }

        private void ButtonBackToModels(object sender, RoutedEventArgs e) {
            if (backToCatalogWindow is not null) backToCatalogWindow();
        }

        private void ButtonCancel(object sender, RoutedEventArgs e) {

        }

        public Action BackToCatalogWindow {
            set {
                backToCatalogWindow = value;
            }
        }
    }
}
