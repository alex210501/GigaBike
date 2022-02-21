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
        private Action cancelOrderCallback = null;
        private Action saveOrderCallback = null;

        public OrderValidationWindow(CatalogModel currentModel) {
            InitializeComponent();
            Text_Box_Estimation_Date.Text = "Estimation Date :";
        }

        public string GetNameCustomer() {
            return NameInput.Text;
        }

        public string GetAddressCustomer() {
            return AddressInput.Text;
        }

        public string GetTVACustomer() {
            return TVAInput.Text;
        }

        public string GetPhoneCustomer() {
            return PhoneInput.Text;
        }

        private void ButtonSavePurchase(object sender, RoutedEventArgs e) {
            if (saveOrderCallback is not null) saveOrderCallback();
        }

        private void ButtonBackToModels(object sender, RoutedEventArgs e) {
            if (backToCatalogWindow is not null) backToCatalogWindow();
        }

        private void ButtonCancel(object sender, RoutedEventArgs e) {
            if (cancelOrderCallback is not null) cancelOrderCallback();
        }

        public Action BackToCatalogWindow {
            set {
                backToCatalogWindow = value;
            }
        }

        public Action CancelOrderCallback {
            set {
                cancelOrderCallback = value;
            }
        }

        public Action SaveOrderCallback {
            set {
                saveOrderCallback = value;
            }
        }
    }
}
