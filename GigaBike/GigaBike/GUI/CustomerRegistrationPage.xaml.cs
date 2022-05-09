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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class CustomerRegistrationPage : Page {
        private Action cancelOrderCallback = null;
        private Action saveOrderCallback = null;

        public CustomerRegistrationPage(CatalogModel currentModel) {
            InitializeComponent();
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

        public bool AreCustomerInfoValid() {
            return (!string.IsNullOrWhiteSpace(GetNameCustomer())) && (!string.IsNullOrWhiteSpace(GetAddressCustomer())) &&
                   (!string.IsNullOrWhiteSpace(GetTVACustomer())) && (!string.IsNullOrWhiteSpace(GetPhoneCustomer()));
        }

        private void ButtonSavePurchase(object sender, RoutedEventArgs e) {
            if (saveOrderCallback is not null) saveOrderCallback();
        }

        private void ButtonCancel(object sender, RoutedEventArgs e) {
            if (cancelOrderCallback is not null) cancelOrderCallback();
        }

        static public List<string> GetData()
        {
            List<string> data = new List<string>();

            data.Add("Afzaal");
            data.Add("Ahmad");
            data.Add("Zeeshan");
            data.Add("Daniyal");
            data.Add("Rizwan");
            data.Add("John");
            data.Add("Doe");
            data.Add("Johanna Doe");
            data.Add("Pakistan");
            data.Add("Microsoft");
            data.Add("Programming");
            data.Add("Visual Studio");
            data.Add("Sofiya");
            data.Add("Rihanna");
            data.Add("Eminem");

            return data;
        }

        private void addItem(string text)
        {
            TextBlock block = new TextBlock();
            int test = 0;

            // Add the text
            block.Text = text;

            // A little style...
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events
            block.MouseLeftButtonUp += (sender, e) =>
            {
                TVAInput.Text = (sender as TextBlock).Text;
                resultStack.Children.Clear();
                resultStack.Children.Remove(block);
                test = 1;
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            if(test == 0)
            {
                resultStack.Children.Add(block);
            }

        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = CustomerRegistrationPage.GetData();

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear
                resultStack.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list
            resultStack.Children.Clear();

            // Add the result
            foreach (var obj in data)
            {
                if (obj.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work
                    addItem(obj);
                    found = true;
                }
            }

            if (!found)
            {
                resultStack.Children.Add(new TextBlock() { Text = "No results found." });
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
