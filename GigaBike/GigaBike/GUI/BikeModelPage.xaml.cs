﻿using System;
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
    /// Interaction logic for BikeModelPage.xaml
    /// </summary>
    public partial class BikeModelPage : Page {
        private Action backToCatalogCallback = null;
        private Action nextButtonCallback = null;
        private CatalogModel currentCatalogModel = null;
        private static string imageFolder = "/GUI/Pictures/";

        public BikeModelPage(CatalogModel currentCatalogModel) {
            InitializeComponent();

            this.currentCatalogModel = currentCatalogModel;
            RefreshModel();
        }

        private void ButtonBackToCatalog(object sender, RoutedEventArgs e) {
            if (backToCatalogCallback is not null) backToCatalogCallback();
        }

        private void ColorSelectionChangedCallback(object sender, SelectionChangedEventArgs e) {
            RefreshBikePrice();
        }

        private void SizeSelectionChangedCallback(object sender, SelectionChangedEventArgs e) {
            RefreshBikePrice();
        }

        private void Text_Input_Quantity(object sender, TextChangedEventArgs e) {
        }

        private void ButtonCommandNext(object sender, RoutedEventArgs e) {
            if (nextButtonCallback is not null) nextButtonCallback();
        }

        private void RefreshModel() {
            AddColor(currentCatalogModel.AvailableColor);
            AddSize(currentCatalogModel.AvailableSize);
            BikeName.Content = currentCatalogModel.Name;
            RefreshBikePrice();
            RefreshBikePicture();
        }

        void RefreshBikePrice() {
            try {
                Color color = GetColor();
                Size size = GetSize();

                if ((color is not null) && (size is not null)) {
                    Bike currentBike = currentCatalogModel.GetBike(color, size);
                    PriceTextBox.Text = currentBike.Price.ToString();
                }
                else
                    PriceTextBox.Text = "0";

                PriceTextBox.Foreground = Brushes.Black; // Put text in black
            }
            catch (BikeNotFoundException) {
                PriceTextBox.Text = "Bike not available";
                PriceTextBox.Foreground = Brushes.Red; // Put text in red
            }
        }

        void RefreshBikePicture() {
            Bike currentBike = currentCatalogModel.GetFirstBike();

            Uri uriImage = new Uri(string.Format("{0}{1}", imageFolder, currentBike.ImagePath), UriKind.Relative);
            BikePicture.Source = new BitmapImage(uriImage);
        }

        private void AddColor(List<Color> colors) {
            foreach (Color colorModel in colors) BikeColor.Items.Add(colorModel);
        }

        private void AddSize(List<Size> sizes) {
            foreach (Size sizeModel in sizes) BikeSize.Items.Add(sizeModel);
        }

        public int GetQuantity() {
            return short.Parse(QuantityText.Text);
        }

        public Color GetColor() {
            return (BikeColor.SelectedItem as Color);
        }

        public Size GetSize() {
            return (BikeSize.SelectedItem as Size);
        }

        public Action BackToCatalogCallback {
            set {
                backToCatalogCallback = value;
            }
        }

        public Action NextButtonCallback {
            set {
                nextButtonCallback = value;
            }
        }
    }
}
