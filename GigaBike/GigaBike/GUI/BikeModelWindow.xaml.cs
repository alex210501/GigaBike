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
using System.Windows.Shapes;

namespace GigaBike {
    public partial class BikeModelWindow : Window {
        private Action backToCatalogCallback = null;
        private CatalogModel currentCatalogModel = null;

        public BikeModelWindow(CatalogModel currentCatalogModel) {
            InitializeComponent();

            this.currentCatalogModel = currentCatalogModel;
            RefreshModel();
        }

        private void ButtonBackToCatalog(object sender, RoutedEventArgs e) {
            if (backToCatalogCallback is not null) backToCatalogCallback();
        }

        private void ListSize1(object sender, RoutedEventArgs e) {
        }

        private void ListSize2(object sender, RoutedEventArgs e) {
        }

        private void ListColor_All(object sender, SelectionChangedEventArgs e) {
        }

        private void ListSize_All(object sender, SelectionChangedEventArgs e) {

        }

        private void Text_Input_Quantity(object sender, TextChangedEventArgs e) {
        }

        private void ButtonCommandNext(object sender, RoutedEventArgs e) {
            OrderValidationWindow OV1 = new OrderValidationWindow();
            OV1.Show();
            this.Hide();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e) {
        }

        private void RefreshModel() {
            AddColor(currentCatalogModel.AvailableColor);
            AddSize(currentCatalogModel.AvailableSize);
            BikeName.Content = currentCatalogModel.Name;
        }

        private void AddColor(List<Color> colors) {
            foreach (Color colorModel in colors) BikeColor.Items.Add(colorModel.Name);
        }

        private void AddSize(List<Size> sizes) {
            foreach (Size sizeModel in sizes) BikeSize.Items.Add(sizeModel.Name);
        }

        public Action BackToCatalogCallback {
            set {
                backToCatalogCallback = value;
            }
        }
    }
}
