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
            // Create LoginWindow instance
            Current.MainWindow = new LoginWindow();
            
            // Define callback
            (Current.MainWindow as LoginWindow).LoginButtonCallback = GoToChoosePathWindow;

            Current.MainWindow.Show();
        }

        public void GoToChoosePathWindow() {
            Current.MainWindow.Hide();

            // Create ChoosePathWindow instance
            Current.MainWindow = new ChoosePathWindow();

            // Define callback
            (Current.MainWindow as ChoosePathWindow).GoToCatalogCallback = GoToCatalogWindow;
            Current.MainWindow.Show();
        }

        public void GoToCatalogWindow() {
            Current.MainWindow.Hide();

            // Refresh the catalog models
            controller.Catalog.RefreshModels();

            // Create CatalogWindow instance
            Current.MainWindow = new CatalogWindow();
            (Current.MainWindow as CatalogWindow).SetCurrentModel(controller.Catalog.GetCurrentModel());
            (Current.MainWindow as CatalogWindow).RefresModel();

            // Define Callbacks
            (Current.MainWindow as CatalogWindow).BackToChoosePathCallback = GoToChoosePathWindow;
            (Current.MainWindow as CatalogWindow).CheckModelCallback = GoToBikeModelWindow;
            (Current.MainWindow as CatalogWindow).NextModelCallback = CatalogWindowNextModelCallback;
            (Current.MainWindow as CatalogWindow).PreviousModelCallback = CatalogWindowPreviousModelCallback;

            Current.MainWindow.Show();
        }

        public void GoToBikeModelWindow() {
            Current.MainWindow.Hide();

            // Create BikeModelWindow instance with a CatalogModel as parameter
            Current.MainWindow = new BikeModelWindow(controller.Catalog.GetCurrentModel());

            // Define callback
            (Current.MainWindow as BikeModelWindow).BackToCatalogCallback = GoToCatalogWindow;
            (Current.MainWindow as BikeModelWindow).NextButtonCallback = GoToOrderValidationWindow;

            Current.MainWindow.Show();
        }

        public void GoToOrderValidationWindow() {
            Current.MainWindow.Hide();

            // Create OrderValidationWindow instance
            Current.MainWindow = new OrderValidationWindow(controller.Catalog.GetCurrentModel());

            // Define callback
            (Current.MainWindow as OrderValidationWindow).BackToCatalogWindow = GoToCatalogWindow;

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
