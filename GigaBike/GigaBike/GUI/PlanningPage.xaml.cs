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
    /// Logique d'interaction pour PlanningPage.xaml
    /// </summary>
    public partial class PlanningPage : Page
    {
        private List<Order> orders;
        private Action goBackToOrderListCallback = null;

        public PlanningPage(List<Order> orders) {
            InitializeComponent();
            this.orders = orders;
            ShowPlanning();

        }

        public Action GoBackToOrderListCallback {
            set {
                goBackToOrderListCallback = value;
            }
        }

        private void ShowPlanning() {
            List<PlanningRow> planningRows = new List<PlanningRow>();

            foreach (Order currentOrder in orders) {
                foreach (BikeOrder currentBikeOrder in currentOrder.Bikes) {
                    PlanningRow currentPlanningRow = new PlanningRow();

                    currentPlanningRow.IdOrder = currentOrder.IdOrder;
                    currentPlanningRow.Bike = currentBikeOrder.Bike.Name;
                    currentPlanningRow.Size = currentBikeOrder.Bike.Size.Name;
                    currentPlanningRow.Color = currentBikeOrder.Bike.Color.Name;
                    currentPlanningRow.deliveryDate = currentBikeOrder.SlotOfBike[0].Date.ToString("dd/MM/yyyy");

                    planningRows.Add(currentPlanningRow);
                }
            }

            DataGridPlanning.ItemsSource = planningRows;
        }

        private void DataGridOrderList(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonGoToPlanning(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSavePlanning(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddToPlanning(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonGoBackToRessource(object sender, RoutedEventArgs e) {
            if (goBackToOrderListCallback is not null) goBackToOrderListCallback();
        }
    }

    public class PlanningRow {
        public int IdOrder { get; set; }
        public string Bike { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string deliveryDate { get; set; }
    }
}
