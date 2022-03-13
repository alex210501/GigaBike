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
using System.ComponentModel;
using System.Diagnostics;

namespace GigaBike
{
    /// <summary>
    /// Logique d'interaction pour PlanningPage.xaml
    /// </summary>
    public partial class PlanningPage : Page {
        private List<Order> ordersToShow;
        private Action goBackToOrderListCallback = null;
        private Action savePlanningCallback = null;
        private List<Slot> slotAvailable;

        public PlanningPage(List<Order> ordersToShow, List<Slot> slotAvailable) {
            InitializeComponent();
            this.ordersToShow = ordersToShow;
            this.slotAvailable = slotAvailable;
            ShowPlanning();

        }

        public Action GoBackToOrderListCallback {
            set {
                goBackToOrderListCallback = value;
            }
        }

        public Action SavePlanningCallback {
            set {
                savePlanningCallback = value;
            }
        }

        public List<Order> OrderToShow {
            get {
                return new List<Order>(ordersToShow);
            }
        }

        private void ShowPlanning() {
            List<PlanningRow> planningRows = new List<PlanningRow>();

            foreach (Order currentOrder in ordersToShow) {
                foreach (BikeOrder currentBikeOrder in currentOrder.Bikes) {
                    PlanningRow currentPlanningRow = new PlanningRow();
                    Slot currentSlot = currentBikeOrder.SlotOfBike[0];

                    currentPlanningRow.IdOrder = currentOrder.IdOrder;
                    currentPlanningRow.IdOrderModel = currentBikeOrder.IdOrderModel;
                    currentPlanningRow.Bike = currentBikeOrder.Bike.Name;
                    currentPlanningRow.Size = currentBikeOrder.Bike.Size.Name;
                    currentPlanningRow.Color = currentBikeOrder.Bike.Color.Name;
                    currentPlanningRow.DeliveryDate = currentSlot.Date;
                    currentPlanningRow.SelectedSlot = currentSlot.SlotNumber;
                    currentPlanningRow.SlotAvailable = GetFreeSlotNumbersByDate(currentPlanningRow.DeliveryDate);
                    currentPlanningRow.IsReady = new List<bool>();
                    currentPlanningRow.SelectedReadyState = currentSlot.IsReady;
                    currentPlanningRow.IsReady.Add(true);
                    currentPlanningRow.IsReady.Add(false);

                    planningRows.Add(currentPlanningRow);
                    IsOrderReady.ItemsSource = currentPlanningRow.IsReady;
                }
            }

            DataGridPlanning.ItemsSource = planningRows.OrderBy(row => row.DeliveryDate).ThenBy(row => row.SelectedSlot).ToList();
        }

        private void DataGridOrderList(object sender, SelectionChangedEventArgs e) {

        }

        private void ButtonGoToPlanning(object sender, RoutedEventArgs e) {

        }

        private void ButtonSavePlanning(object sender, RoutedEventArgs e) {
            if (savePlanningCallback is not null) savePlanningCallback();
        }

        private void ButtonAddToPlanning(object sender, RoutedEventArgs e) {

        }

        private void ButtonGoBackToRessource(object sender, RoutedEventArgs e) {
            if (goBackToOrderListCallback is not null) goBackToOrderListCallback();
        }

        private void OnValueReadyStateChanged(object sender, RoutedEventArgs e) {
            PlanningRow selectedPlanningRow = DataGridPlanning.SelectedItem as PlanningRow;
            int idOrder = selectedPlanningRow.IdOrder;
            int idOrderModel = selectedPlanningRow.IdOrderModel;
            bool isSlotReady = selectedPlanningRow.SelectedReadyState;

            Order selectedOrder = ordersToShow.Find(o => o.IdOrder == idOrder);
            BikeOrder selectedBikeOrder = selectedOrder.Bikes.Find(o => o.IdOrderModel == idOrderModel);
            selectedBikeOrder.SetReadyState(isSlotReady);
        }

        private List<int> GetFreeSlotNumbersByDate(DateTime date) {
            return slotAvailable.FindAll(slot => slot.Date == date).Select(slot => slot.SlotNumber).ToList();
        }
    }

    public class PlanningRow {
        public int IdOrder { get; set; }
        public int IdOrderModel { get; set; }
        public string Bike { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<int> SlotAvailable { get; set; }
        public int SelectedSlot { get; set; }
        public List<bool> IsReady { get; set; }
        public bool SelectedReadyState { get; set; }
    }
}
