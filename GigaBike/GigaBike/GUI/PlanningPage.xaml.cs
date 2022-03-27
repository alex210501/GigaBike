using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private List<PlanningRow> planningRows;
        private Action goBackToOrderListCallback = null;
        private Action savePlanningCallback = null;
        private Action setSlotForEveryBikeCallback = null;
        private Action<DateTime> saveDateCallback = null;
        private Action<int> slotChangedCallback = null;
        private bool comboboxInteraction = false;

        public PlanningPage(List<Order> ordersToShow, List<Slot> slotAvailable) {
            InitializeComponent();
            this.ordersToShow = ordersToShow;
            this.planningRows = new List<PlanningRow>();
        }

        public PlanningRow GetCurrentPlanningRow() {
            return DataGridPlanning.SelectedItem as PlanningRow;
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

        public Action<DateTime> SaveDateCallback {
            set {
                saveDateCallback = value;
            }
        }

        public Action SetSlotForEveryBikeCallback {
            set {
                setSlotForEveryBikeCallback = value;
            }
        }

        public Action<int> SlotChangedCallback {
            set {
                slotChangedCallback = value;
            }
        }

        public List<Order> OrderToShow {
            get {
                return new List<Order>(ordersToShow);
            }
        }

        public List<PlanningRow> PlanningRows {
            get {
                return planningRows;
            }
        }

        public void ShowPlanning() {
            planningRows = new List<PlanningRow>();

            foreach (Order currentOrder in ordersToShow) {
                foreach (BikeOrder currentBikeOrder in currentOrder.Bikes) {
                    PlanningRow currentPlanningRow = new PlanningRow();

                    // If the bike have specific slots
                    if (currentBikeOrder.SlotOfBike.Count > 0) {
                        Slot currentSlot = currentBikeOrder.SlotOfBike[0];

                        currentPlanningRow.IdOrder = currentOrder.IdOrder;
                        currentPlanningRow.IdOrderModel = currentBikeOrder.IdOrderModel;
                        currentPlanningRow.Bike = currentBikeOrder.Bike;
                        currentPlanningRow.DeliveryDate = currentSlot.Date;
                        currentPlanningRow.SelectedSlot = currentSlot.SlotNumber;
                        currentPlanningRow.SlotAvailable = new List<int>();
                        currentPlanningRow.IsReady = new List<bool>();
                        currentPlanningRow.SelectedReadyState = currentSlot.IsReady;
                        currentPlanningRow.IsReady.Add(true);
                        currentPlanningRow.IsReady.Add(false);

                        planningRows.Add(currentPlanningRow);
                        IsOrderReady.ItemsSource = currentPlanningRow.IsReady;
                    }
                }
            }

            if (setSlotForEveryBikeCallback is not null) setSlotForEveryBikeCallback();

            planningRows = planningRows.OrderBy(row => row.DeliveryDate).ThenBy(row => row.SelectedSlot).ToList();
            DataGridPlanning.ItemsSource = planningRows;
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
            PlanningRow selectedPlanningRow = GetCurrentPlanningRow();
            int idOrder = selectedPlanningRow.IdOrder;
            int idOrderModel = selectedPlanningRow.IdOrderModel;
            bool isSlotReady = selectedPlanningRow.SelectedReadyState;

            Order selectedOrder = ordersToShow.Find(o => o.IdOrder == idOrder);
            BikeOrder selectedBikeOrder = selectedOrder.Bikes.Find(o => o.IdOrderModel == idOrderModel);
            selectedBikeOrder.SetReadyState(isSlotReady);
        }

        private void SelectedDateChanged(object sender, RoutedEventArgs e) {
            DatePicker dpick = sender as DatePicker;
            DateTime selectedDate = dpick.SelectedDate.Value.Date; 
            PlanningRow selectedPlanningRow = GetCurrentPlanningRow();

            if (selectedPlanningRow.DeliveryDate != selectedDate) {
                if (saveDateCallback is not null) saveDateCallback(selectedDate);

                selectedPlanningRow.DeliveryDate = selectedDate;
                DataGridPlanning.ItemsSource = new List<int>();
                DataGridPlanning.ItemsSource = planningRows;
                // DataGridPlanning.IsReadOnly = false;
            }
        }

        private void OnSlotChanged(object sender, RoutedEventArgs e) {
            if (comboboxInteraction) {
                ComboBox slotCombobox = sender as ComboBox;
                int slotNumberSelected = Convert.ToInt32(slotCombobox.SelectedItem);
                PlanningRow selectedPlanningRow = GetCurrentPlanningRow();

                if (slotNumberSelected > 0) {
                    if (slotChangedCallback is not null) slotChangedCallback(slotNumberSelected);
                }

                comboboxInteraction = false;
            }
        }

        private void DataGridPlanning_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e) {
            comboboxInteraction = true;
        }
    }

    public class PlanningRow {
        public int IdOrder { get; set; }
        public int IdOrderModel { get; set; }
        public Bike Bike { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<int> SlotAvailable { get; set; }
        public int SelectedSlot { get; set; }
        public List<bool> IsReady { get; set; }
        public bool SelectedReadyState { get; set; }
    }
}
