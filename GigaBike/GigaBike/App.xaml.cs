using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace GigaBike {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private Controller controller;

        App() {
            controller = new Controller();

            try {
                controller.Init();
            }
            catch (MySql.Data.MySqlClient.MySqlException) {
                Trace.WriteLine("No database connection !");
            }
        }

		protected override  void OnStartup(StartupEventArgs e) {
            // Create LoginWindow instance
            Current.MainWindow = new LoginWindow();
            
            // Define callback
            (Current.MainWindow as LoginWindow).LoginButtonCallback = LoginButtonCallback;

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
            (Current.MainWindow as CatalogWindow).RefreshModel();

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
            (Current.MainWindow as BikeModelWindow).NextButtonCallback = AddToOrderCallback;

            Current.MainWindow.Show();
        }

        public void GoToOrderValidationWindow() {
            Current.MainWindow.Hide();

            // Create OrderValidationWindow instance
            Current.MainWindow = new OrderValidationWindow(controller.Catalog.GetCurrentModel());

            // Define callback
            (Current.MainWindow as OrderValidationWindow).BackToCatalogWindow = GoToCatalogWindow;
            (Current.MainWindow as OrderValidationWindow).CancelOrderCallback = CancelOrderCallback;
            (Current.MainWindow as OrderValidationWindow).SaveOrderCallback = SaveOrderCallback;

            Current.MainWindow.Show();
        }

        public void GoToOrderConfirmationWindow()
        {
            Current.MainWindow.Hide();

            // Create OrderValidationWindow instance
            Current.MainWindow = new ConfirmationOrderWindow();

            (Current.MainWindow as ConfirmationOrderWindow).SetCurrentOrder(controller.Order);

            // Define callback
            (Current.MainWindow as ConfirmationOrderWindow).ValidateOrderCallback = ValidateOrderCallback;
            (Current.MainWindow as ConfirmationOrderWindow).CancelOrderCallback = CancelOrderCallback;

            Current.MainWindow.Show();
        }

        public void LoginButtonCallback()
        {
            string username = (Current.MainWindow as LoginWindow).getText_Input_Username();
            string password = (Current.MainWindow as LoginWindow).getText_Input_Password();

            /*if (controller.Login.CheckUser(username, password))
            {
                GoToChoosePathWindow();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }*/
            GoToChoosePathWindow();

        }

        public CatalogModel CatalogWindowNextModelCallback() {
            controller.Catalog.NextModel();

            return controller.Catalog.GetCurrentModel();
        }

        public CatalogModel CatalogWindowPreviousModelCallback() {
            controller.Catalog.PreviousModel();

            return controller.Catalog.GetCurrentModel();
        }

        public void AddToOrderCallback() {
            try {
                // Get information from the display
                Color colorBike = (Current.MainWindow as BikeModelWindow).GetColor();
                Size sizeBike = (Current.MainWindow as BikeModelWindow).GetSize();
                int quantity = (Current.MainWindow as BikeModelWindow).GetQuantity();

                // Get the bike selected
                Bike currentBikeModel = controller.Catalog.GetSelectedBike(colorBike, sizeBike);

                // Add the bike to the order list
                controller.Order.AddBike(new Bike(currentBikeModel), quantity);

                // Go to the Order Window
                GoToOrderValidationWindow();

                foreach (BikeOrder bikeOrder in controller.Order.Bikes) {
                    Trace.WriteLine(string.Format("Bike : {0}, color : {1}, size: {2}, quantity : {3}", bikeOrder.Bike.Name, bikeOrder.Bike.Color.Name,
                                    bikeOrder.Bike.Size.Name, bikeOrder.Quantity));
                }
            }
            catch (FormatException) {
                MessageBox.Show("The quantity must be an integer !");
            }
            catch (BikeNotFoundException e) {
                MessageBox.Show(e.Message);
            }
        }

        public void CancelOrderCallback() {
            controller.Order.Clear();
            GoToCatalogWindow();
        }

        public void SaveOrderCallback() {
            if (Current.MainWindow is not OrderValidationWindow) {
                MessageBox.Show("This callback is for the order validation window !");
                return;
            }

            if ((Current.MainWindow as OrderValidationWindow).AreCustomerInfoValid() == false) {
                MessageBox.Show("You must set all the client informations to save the order !");
                return;
            }

            Customer orderCustomer = new Customer() {
                Name = (Current.MainWindow as OrderValidationWindow).GetNameCustomer(),
                Address = (Current.MainWindow as OrderValidationWindow).GetAddressCustomer(),
                TVA = (Current.MainWindow as OrderValidationWindow).GetTVACustomer(),
                Phone = (Current.MainWindow as OrderValidationWindow).GetPhoneCustomer()
            };

            controller.Order.Save(orderCustomer);
            controller.SetDateForOrderBike();

            GoToOrderConfirmationWindow();
        }

        void ValidateOrderCallback() {
            controller.Order.Validate();
            controller.Order.Clear();
            GoToCatalogWindow();
        }
    }
}
