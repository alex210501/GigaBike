using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace GigaBike {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private Controller controller;

        App() {
            controller = new Controller();
        }

		protected override  void OnStartup(StartupEventArgs e) {
            Current.MainWindow = new LoginWindow();
            
            if (Current.MainWindow is LoginWindow)
                (Current.MainWindow as LoginWindow).LoginButtonCallback = GoToCatalogWindow;
            Current.MainWindow.Show();
        }

        public void GoToChoosePathWindow() {
            Current.MainWindow.Hide();
            Current.MainWindow = new ChoosePathWindow();
            Current.MainWindow.Show();
        }

        public void GoToCatalogWindow() {
            controller.Catalog.RefreshModels();
            Current.MainWindow.Hide();
            Current.MainWindow = new CatalogWindow();
            (Current.MainWindow as CatalogWindow).SetCurrentModel(controller.Catalog.GetCurrentModel());
            (Current.MainWindow as CatalogWindow).RefresModel();
            (Current.MainWindow as CatalogWindow).NextModelCallback = CatalogWindowNextModelCallback;
            (Current.MainWindow as CatalogWindow).PreviousModelCallback = CatalogWindowPreviousModelCallback;

            Current.MainWindow.Show();
        }

        public void GoToBikeModelWindow(Bike bike) {
            Current.MainWindow.Hide();
            Current.MainWindow = new BikeModelWindow();
            Current.MainWindow.Show();
        }

        public void GoToOrderValidationWindow() {
            Current.MainWindow.Hide();
            Current.MainWindow = new OrderValidationWindow();
            Current.MainWindow.Show();
        }

        public CatalogModel CatalogWindowNextModelCallback() {
            controller.Catalog.NextModel();

            return controller.Catalog.GetCurrentModel();
        }

        public CatalogModel CatalogWindowPreviousModelCallback() {
            controller.Catalog.PreviousModel();

            return controller.Catalog.GetCurrentModel();
        }
    }
}
