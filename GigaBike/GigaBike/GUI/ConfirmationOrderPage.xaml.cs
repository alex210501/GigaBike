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
    /// Interaction logic for ConfirmationOrderPage.xaml
    /// </summary>
    public partial class ConfirmationOrderPage : Page {
        private Action validateOrderCallback = null;
        private Action cancelOrderCallback = null;

        public ConfirmationOrderPage() {
            InitializeComponent();
        }

        private void ButtonCancelCommand(object sender, RoutedEventArgs e) {
            if (cancelOrderCallback is not null) cancelOrderCallback();
        }

        private void ButtonValidateCommand(object sender, RoutedEventArgs e) {
            if (validateOrderCallback is not null) validateOrderCallback();
        }

        private void TableRecapCommand(object sender, SelectionChangedEventArgs e) {
            //Datagrid
        }

        public void SetCurrentOrder(Order CurrentOrder) {
            List<OrderRecapGrid> ordersRecap = new List<OrderRecapGrid>();

            foreach (BikeOrder bikeOrder in CurrentOrder.Bikes) {
                OrderRecapGrid currentOrderRecapGrid = new OrderRecapGrid(0);
                Bike currentBike = bikeOrder.Bike;

                currentOrderRecapGrid.BikeName = currentBike.Name;
                currentOrderRecapGrid.OrderPrice = bikeOrder.Bike.Price;
                currentOrderRecapGrid.Color = currentBike.Color.Name;
                currentOrderRecapGrid.Size = currentBike.Size.Name;
                currentOrderRecapGrid.Quantity = 1; //bikeOrder.Quantity;

                ordersRecap.Add(currentOrderRecapGrid);
            }


            TableRecap.ItemsSource = ordersRecap;
        }

        public void SetDeliveryDate(DateTime deliveryDate) {
            DeliveryDateLabel.Content = deliveryDate.ToString("dd/MM/yyyy");
        }

        public Action ValidateOrderCallback {
            set {
                validateOrderCallback = value;
            }
        }

        public Action CancelOrderCallback {
            set {
                cancelOrderCallback = value;
            }
        }
    }

    public class OrderRecapGrid {
        public int CommandNumber { get; set; }

        public string BikeName { get; set; }
        public int OrderPrice { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }

        public OrderRecapGrid(int CommandNumber) {
            this.CommandNumber = CommandNumber;
        }
    }
}
