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

            List<User> users = new List<User>();
            foreach (BikeOrder bikeOrder in CurrentOrder.Bikes)
                users.Add(new User() { Id = bikeOrder.Bike.IdBike, Name = String.Join(',', bikeOrder.Bike.Name), Quantity =  bikeOrder.Quantity, Price = bikeOrder.Price});


            TableRecap.ItemsSource = users;
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
    public class User
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
