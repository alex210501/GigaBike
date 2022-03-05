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

namespace GigaBike
{
    /// <summary>
    /// Logique d'interaction pour OrderListPage.xaml
    /// </summary>
    public partial class PM_OrderListPage : Page
    {
        private Action goToPlanningCallback = null;
        private Action goBackToRessourceCallback = null;
        private List<Order> ordersToShow;

        public PM_OrderListPage(List<Order> orders) {
            InitializeComponent();
            ordersToShow = orders;
            ShowOrders();
        }

        private void ButtonGoToPlanning(object sender, RoutedEventArgs e) {
            if (goToPlanningCallback is not null) goToPlanningCallback();
        }

        private void ButtonGoBackToRessource(object sender, RoutedEventArgs e) {
            if (goBackToRessourceCallback is not null) goBackToRessourceCallback();
        }

        public void ShowOrders() {
            List<OrderRow> orderRows = new List<OrderRow>();

            foreach (Order currentOrder in ordersToShow) {
                var idBikeWithBikeAndQuantity = currentOrder.Bikes.GroupBy(o => o.Bike.IdBike).ToDictionary(g => g.Key, g => new { Bike = g.First().Bike, Quantity = g.Count() });
                foreach (var bikeGroup in idBikeWithBikeAndQuantity) {
                    OrderRow currentRow = new OrderRow();
                    Bike currentBike = bikeGroup.Value.Bike;

                    currentRow.IdOrder = currentOrder.IdOrder;
                    currentRow.Bike = currentBike.Name;
                    currentRow.Quantity = bikeGroup.Value.Quantity;
                    currentRow.Size = currentBike.Size.Name;
                    currentRow.Color = currentBike.Color.Name;
                    currentRow.Price = currentOrder.Price;

                    orderRows.Add(currentRow);
                }
            }

            DataGridOrderList.ItemsSource = orderRows;
        }

        public Action GoToPlanningCallback {
            set {
                goToPlanningCallback = value;
            }
        }

        public Action GoBackToRessourceCallback {
            set {
                goBackToRessourceCallback = value;
            }
        }
    }

    public class OrderRow {
        public int IdOrder { get; set; }
        public string Bike { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
    }
}
