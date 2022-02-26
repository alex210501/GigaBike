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
using System.Diagnostics;

namespace GigaBike {
    /// <summary>
    /// Interaction logic for CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page {
        Func<CatalogModel> nextModelCallback = null;
        Func<CatalogModel> previousModelCallback = null;
        Action backToChoosePathCallback = null;
        Action checkModelCallback = null;
        private static string imageFolder = "/GUI/Pictures/";

        CatalogModel catalogModel = null;

        public CatalogPage() {
            InitializeComponent();
        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e) {
            if (previousModelCallback is not null) catalogModel = previousModelCallback();

            RefreshModel();
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e) {
            if (nextModelCallback is not null) catalogModel = nextModelCallback();

            RefreshModel();
        }

        private void ButtonBackToChoose(object sender, RoutedEventArgs e) {
            if (backToChoosePathCallback is not null) backToChoosePathCallback();
        }

        private void ButtonCheck1(object sender, RoutedEventArgs e) {
            if (checkModelCallback is not null) checkModelCallback();
        }

        public void SetCurrentModel(CatalogModel currentModel) {
            catalogModel = currentModel;
        }

        public void RefreshModel() {
            NameBike.Text = catalogModel.Name;

            // Refresh the bike picture
            Uri uriImage = new Uri(string.Format("{0}{1}", imageFolder, catalogModel.GetFirstBike().ImagePath), UriKind.Relative);
            BikePicture.Source = new BitmapImage(uriImage);

            Trace.WriteLine(BikePicture.Source);
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

        public Action CheckModelCallback {
            set {
                checkModelCallback = value;
            }
        }
    }
}
