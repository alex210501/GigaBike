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
            BikeModelPage bikeModelPage = new BikeModelPage(controller.Catalog.GetCurrentModel());

            // Create BikeModelWindow instance with a CatalogModel as parameter
            Current.MainWindow.Content = bikeModelPage;

            // Define callback
            bikeModelPage.BackToCatalogCallback = GoToCatalogWindow;
            bikeModelPage.NextButtonCallback = AddToOrderCallback;
        }

        public void GoToRegistrationCustomerWindow() {
            CustomerRegistrationPage customerRegistrationPage = new CustomerRegistrationPage(controller.Catalog.GetCurrentModel());

            // Create OrderValidationWindow instance
            Current.MainWindow.Content = customerRegistrationPage;

            // Define callback
            customerRegistrationPage.BackToCatalogWindow = GoToCatalogWindow;
            customerRegistrationPage.CancelOrderCallback = CancelOrderCallback;
            customerRegistrationPage.SaveOrderCallback = SaveOrderCallback;
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
                BikeModelPage bikeModelPage =(Current.MainWindow.Content as BikeModelPage);

                // Get information from the display
                Color colorBike = bikeModelPage.GetColor();
                Size sizeBike = bikeModelPage.GetSize();
                int quantity = bikeModelPage.GetQuantity();

                // Get the bike selected
                Bike currentBikeModel = controller.Catalog.GetSelectedBike(colorBike, sizeBike);

                // Add the bike to the order list
                controller.Order.AddBike(new Bike(currentBikeModel), quantity);

                // Go to the Order Window
                GoToRegistrationCustomerWindow();

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
