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
    /// Logique d'interaction pour OrderPartPage.xaml
    /// </summary>
    public partial class OrderPartPage : Page {
        private Action createPurchaseCallback = null;
        private Action orderPartCallback = null;
        private Action buttonBackCallback = null;

        List<PurchaseRow> purchasesRows;
        private PurchaseOrderPart currentPurchase;
        public OrderPartPage() {
            InitializeComponent();
            this.currentPurchase = null;
            purchasesRows = new List<PurchaseRow>();
        }

        public Action CreatePurchaseCallback {
            set {
                createPurchaseCallback = value;
            }
        }

        public Action OrderPartCallback {
            set {
                orderPartCallback = value;
            }
        }

        public Action ButtonBackCallback {
            set {
                buttonBackCallback = value;
            }
        }

        public void SetCurrentPurchase(PurchaseOrderPart currentPurchase) {
            this.currentPurchase = currentPurchase;
        }

        public void SelectCurrentPurchasePart() {
            // If there's no current purchases registered, don't search through the list
            if (currentPurchase is null) return;

            PurchaseRow currentPurchaseRow = purchasesRows.Find(p => p.IdPurchase == currentPurchase.IdPurchaseOrderPart);

            if (currentPurchaseRow is not null)
                RefreshPartGrid(currentPurchaseRow.PartToOrder);
        }

        public void RefreshPurchaseGrid() {
            // Clear the purchases rows
            purchasesRows.Clear();

            // Add the current purchase if it's not null
            if (currentPurchase is not null) {
                PurchaseRow currentPurchaseRow = new PurchaseRow();

                currentPurchaseRow.IdPurchase = currentPurchase.IdPurchaseOrderPart;
                currentPurchaseRow.PartToOrder = new List<OrderPart>(currentPurchase.OrderParts);
                purchasesRows.Add(currentPurchaseRow);
            }

            DataGridPurchase.ItemsSource = purchasesRows;
        }

        public void RefreshPartGrid(List<OrderPart> orderParts) {
            List<PartRow> partRows = new List<PartRow>();

            foreach (OrderPart currentOrderPart in orderParts) {
                PartRow currentPartRow = new PartRow();

                currentPartRow.PartName = currentOrderPart.Part.NamePart;
                currentPartRow.PartColor = currentOrderPart.Part.Color;
                currentPartRow.PartSize = currentOrderPart.Part.Size;
                currentPartRow.QuantityToOrder = currentOrderPart.QuantityToOrder;

                partRows.Add(currentPartRow);
            }

            DataGridParts.ItemsSource = partRows;
        }

        private void ButtonCreateNewPurchases(object sender, RoutedEventArgs e) {
            if (createPurchaseCallback is not null) createPurchaseCallback();
        }

        private void ButtonOrderPart(object sender, RoutedEventArgs e) {
            if (orderPartCallback is not null) orderPartCallback();
        }

        private void ButtonBack(object sender, RoutedEventArgs e) {
            if (buttonBackCallback is not null) buttonBackCallback();
        }
    }

    public class PurchaseRow {
        public int IdPurchase { get; set; }
        public List<OrderPart> PartToOrder { get; set; }
        public DateTime DatePurchase { get; set; }
    }

    public class PartRow {
        public string PartName { get; set; }
        public Color PartColor { get; set; }
        public Size PartSize { get; set; }
        public int QuantityToOrder { get; set; }
        // public int Location { get; set; }
    }
}
