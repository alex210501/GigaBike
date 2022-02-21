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
    public partial class ConfirmationOrderWindow : Window {
        private Action validateOrderCallback = null;
        private Action cancelOrderCallback = null;

        public ConfirmationOrderWindow() {
            InitializeComponent();
        }

        private void ButtonCancelCommand(object sender, RoutedEventArgs e)
        {
            if (cancelOrderCallback is not null) cancelOrderCallback();
        }

        private void ButtonValidateCommand(object sender, RoutedEventArgs e)
        {
            if (validateOrderCallback is not null) validateOrderCallback();
        }

        private void TableRecapCommand(object sender, SelectionChangedEventArgs e)
        {
            //Datagrid
        }

        public void SetCurrentOrder(Order CurrentOrder)
        {
            List<OrderRecap> ordersRecap = new List<OrderRecap>();

            foreach (BikeOrder bikeOrder in CurrentOrder.Bikes) {
                OrderRecap currentOrderRecap = new OrderRecap(0);
                Bike currentBike = bikeOrder.Bike;

                currentOrderRecap.BikeName = currentBike.Name;
                currentOrderRecap.OrderPrice = bikeOrder.Price;
                currentOrderRecap.Color = currentBike.Color.Name;
                currentOrderRecap.Size = currentBike.Size.Name;
                currentOrderRecap.Quantity = bikeOrder.Quantity;

                ordersRecap.Add(currentOrderRecap);
            }


            TableRecap.ItemsSource = ordersRecap;
        }

        public Action ValidateOrderCallback
        {
            set
            {
                validateOrderCallback = value;
            }
        }

        public Action CancelOrderCallback
        {
            set
            {
                cancelOrderCallback = value;
            }
        }
    }
    public class OrderRecap
    {
        public int CommandNumber { get; set; }

        public string BikeName { get; set; }
        public int OrderPrice { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }

        public OrderRecap(int CommandNumber) {
            this.CommandNumber = CommandNumber;
        }
    }
}
