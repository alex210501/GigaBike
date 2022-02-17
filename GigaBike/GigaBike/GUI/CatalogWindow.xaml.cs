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
using System.Diagnostics;

namespace GigaBike {
    public partial class CatalogWindow : Window {
        Func<CatalogModel> nextModelCallback = null;
        Func<CatalogModel> previousModelCallback = null;
        Action backToChoosePathCallback = null;

        CatalogModel catalogModel = null;

        public CatalogWindow() {
            InitializeComponent();
            NameBike.Text = "City Bike";
        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e) {
            if (previousModelCallback is not null) catalogModel = previousModelCallback();

            RefresModel();
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e) {
            if (nextModelCallback is not null) catalogModel = nextModelCallback();

            RefresModel();
        }

        private void ButtonBackToChoose(object sender, RoutedEventArgs e) {
            if (backToChoosePathCallback is not null) backToChoosePathCallback();
        }

        private void ButtonCheck1(object sender, RoutedEventArgs e) {
            /*BikeModelWindow cb1 = new BikeModelWindow(); // a changer lorsque l'on va appeler la base ==> création de form automatique avec les informations => besoin création template
            cb1.Show();
            this.Hide();*/
        }

        public void SetCurrentModel(CatalogModel currentModel) {
            catalogModel = currentModel;
        }

        public void RefresModel() {
            NameBike.Text = catalogModel.Name;
        }

        public Func<CatalogModel> NextModelCallback {
            set {
                nextModelCallback = value;
            }
        }

        public Func<CatalogModel> PreviousModelCallback {
            set {
                previousModelCallback = value;
            }
        }

        public Action BackToChoosePathCallback {
            set {
                backToChoosePathCallback = value;
            }
        }
    }
}
