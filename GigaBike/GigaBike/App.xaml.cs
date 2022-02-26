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
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Content = new LoginPage();
            
            // Define callback
            (Current.MainWindow.Content as LoginPage).LoginButtonCallback = LoginButtonCallback;

            Current.MainWindow.Show();
        }

        public void GoToChoosePathWindow() {
            // Create ChoosePathWindow instance
            Current.MainWindow.Content = new ChoosePathPage();

            // Define callback
            (Current.MainWindow.Content as ChoosePathPage).GoToCatalogCallback = GoToCatalogWindow;
            // Current.MainWindow.Show();
        }

        public void GoToCatalogWindow() {
            // Refresh the catalog models
            controller.Catalog.RefreshModels();

            // Create CatalogWindow instance
            CatalogPage catalogPage = new CatalogPage();

            Current.MainWindow.Content = catalogPage;
            catalogPage.SetCurrentModel(controller.Catalog.GetCurrentModel());
            catalogPage.RefreshModel();

            // Define Callbacks
            catalogPage.BackToChoosePathCallback = GoToChoosePathWindow;
            catalogPage.CheckModelCallback = GoToBikeModelWindow;
            catalogPage.NextModelCallback = CatalogWindowNextModelCallback;
            catalogPage.PreviousModelCallback = CatalogWindowPreviousModelCallback;
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
            // Get the delivery date
            DateTime deliveryDate = controller.Planning.GetDeliveryDate(controller.Order.IdOrder);

            Current.MainWindow.Hide();

            // Create OrderValidationWindow instance
            Current.MainWindow = new ConfirmationOrderWindow();

            (Current.MainWindow as ConfirmationOrderWindow).SetCurrentOrder(controller.Order);
            (Current.MainWindow as ConfirmationOrderWindow).SetDeliveryDate(deliveryDate);
            Trace.WriteLine(string.Format("Delivery date : {0}", deliveryDate));

            // Define callback
            (Current.MainWindow as ConfirmationOrderWindow).ValidateOrderCallback = ValidateOrderCallback;
            (Current.MainWindow as ConfirmationOrderWindow).CancelOrderCallback = CancelOrderCallback;

            Current.MainWindow.Show();
        }

        public void LoginButtonCallback()
        {
            if (Current.MainWindow.Content is not LoginPage)
                throw new FormatException("The current page is not a LoginPage");
                
            LoginPage loginPage = Current.MainWindow.Content as LoginPage;

            string username = loginPage.getText_Input_Username();
            string password = loginPage.getText_Input_Password();

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
            controller.SetCurrentIdOrder();
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
