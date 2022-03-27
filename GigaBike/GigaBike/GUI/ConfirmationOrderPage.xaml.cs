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
        private Action backToCatalogWindow = null;

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

        public void SetCurrentOrder(Order currentOrder) {
            List<OrderRecapGrid> ordersRecap = new List<OrderRecapGrid>();
            var idBikeWithBikeAndQuantity = currentOrder.Bikes.GroupBy(o => o.Bike.IdBike).ToDictionary(g => g.Key, g => new { Bike = g.First().Bike, Quantity = g.Count() });

            foreach (var bikeGroup in idBikeWithBikeAndQuantity) {
                OrderRecapGrid currentOrderRecapGrid = new OrderRecapGrid(0);
                Bike currentBike = bikeGroup.Value.Bike;
                int bikeQuantity = bikeGroup.Value.Quantity;

                currentOrderRecapGrid.BikeName = currentBike.Name;
                currentOrderRecapGrid.OrderPrice = currentBike.Price * bikeQuantity;
                currentOrderRecapGrid.Color = currentBike.Color.Name;
                currentOrderRecapGrid.Size = currentBike.Size.Name;
                currentOrderRecapGrid.Quantity = bikeQuantity;

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
        private void ButtonBackToModels(object sender, RoutedEventArgs e)
        {
            if (backToCatalogWindow is not null) backToCatalogWindow();
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
