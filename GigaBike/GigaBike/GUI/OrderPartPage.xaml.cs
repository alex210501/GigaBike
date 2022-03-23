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

        private PurchaseOrderPart currentPurchase;
        public OrderPartPage(PurchaseOrderPart currentPurchase) {
            InitializeComponent();
            this.currentPurchase = currentPurchase;
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

        public void RefreshPartGrid() {
            List<PartRow> partRows = new List<PartRow>();

            foreach(OrderPart orderPart in currentPurchase.OrderParts) {
                PartRow currentPartRow = new PartRow();

                currentPartRow.PartName = orderPart.Part.NamePart;
                currentPartRow.PartColor = orderPart.Part.Color;
                currentPartRow.PartSize = orderPart.Part.Size;
                currentPartRow.QuantityToOrder = orderPart.QuantityToOrder;

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

    public class PartRow {

        public string PartName { get; set; }
        public Color PartColor { get; set; }
        public Size PartSize { get; set; }
        public int QuantityToOrder { get; set; }
        // public int Location { get; set; }
    }
}
