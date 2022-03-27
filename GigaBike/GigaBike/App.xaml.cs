using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Diagnostics;
using MySql.Data.MySqlClient;

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
            (Current.MainWindow.Content as ChoosePathPage).GoToRessourcesCallback = GoToRessoucesPage;
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

        public void GoToOrderConfirmationWindow() {
            // Get the delivery date
            DateTime deliveryDate = controller.Order.DeliveryDate;

            ConfirmationOrderPage confirmationOrderPage = new ConfirmationOrderPage();

            // Create OrderValidationWindow instance
            Current.MainWindow.Content = confirmationOrderPage;

            confirmationOrderPage.SetCurrentOrder(controller.Order);
            confirmationOrderPage.SetDeliveryDate(deliveryDate);

            // Define callback
            confirmationOrderPage.ValidateOrderCallback = ValidateOrderCallback;
            confirmationOrderPage.CancelOrderCallback = CancelOrderCallback;

            Current.MainWindow.Show();
        }

        public void GoToPiecesStockPage() {
            PiecesStockPage piecesStockPage = new PiecesStockPage();

            // Create PiecesStockPage instance
            Current.MainWindow.Content = piecesStockPage;
            piecesStockPage.SetStockList(controller.Stock); //à retirer après imlémentation

            // Define callback
            piecesStockPage.GoBackToStockCallback = GoToStockPage;
            piecesStockPage.GoToOrderPiecesCallback = GoToOrderPartPageCallback;
        }

        public void GoToPlanningPage() {
            PlanningPage planningPage = new PlanningPage(controller.OrdersRegistered, controller.Planning.GetAllFreeSlot());

            // Create PlanningPage instance
            Current.MainWindow.Content = planningPage;

            // Define callback
            planningPage.GoBackToOrderListCallback = GoToOrderListPage;
            planningPage.SavePlanningCallback = SavePlanningCallback;
            planningPage.SaveDateCallback = SetSlotForBikeInPlanningPage;
            planningPage.SetSlotForEveryBikeCallback = SetSlotForEveryPlanningDate;
            planningPage.SlotChangedCallback = SlotChangedOnPlanningPage;

            planningPage.ShowPlanning();
        }

        public void GoToOrderListPage() {
            PM_OrderListPage orderListPage = new PM_OrderListPage(controller.OrdersRegistered);

            // Create the OrderListPage instance
            Current.MainWindow.Content = orderListPage;

            // Define callbacks
            orderListPage.GoBackToRessourceCallback = GoToRessoucesPage;
            orderListPage.GoToPlanningCallback = GoToPlanningPage;
        }

        public void GoToOrderPiecesPage() {
            PM_OrderPiecesPage orderPiecePage = new PM_OrderPiecesPage();

            // Creat the OrderPiecesPage instance
            Current.MainWindow.Content = orderPiecePage;
        }

        public void GoToRessoucesPage() {
            PM_RessourcesPage ressourcePage = new PM_RessourcesPage();

            // Create the RessourcesPage instance
            Current.MainWindow.Content = ressourcePage;

            // Define callback
            ressourcePage.GoToChoosePathCallback = GoToChoosePathWindow;
            ressourcePage.GoToOrderListCallback = GoToOrderListCallback;
            ressourcePage.GoToStockCallback = GoToStockPage;
        }

        public void GoToStockPage() {
            StockPage stockPage = new StockPage();

            // Create the StockPage instance
            Current.MainWindow.Content = stockPage;

            // Define callback
            stockPage.GoBackToRessourcesCallback = GoToRessoucesPage;
            stockPage.GoToBikeStockCallback = GoToBikeStockPage;
            stockPage.GoToPiecesStockCallback = GoToStockPageCallback;
        }

        public void GoToChoosePathWorkerPage() {
            ChoosePathWorkerPage choosePathWorkerPage = new ChoosePathWorkerPage();

            // Create the ChoosePathWorkerPage instance
            Current.MainWindow.Content = choosePathWorkerPage;
        }

        public void GoToBikeStockPage() {
            
            BikeStockPage bikeStockPage = new BikeStockPage(controller.BikeInStock.getBikeStock);
            // Create the BikeStockPage instance
            Current.MainWindow.Content = bikeStockPage;

            // Define callback
            bikeStockPage.GoBackToStockCallback = GoToStockPage;
            bikeStockPage.GoToAddBikeToStockCallback = GoToAddBikeToStockPage;
        }

        public void GoToOrderPartPage() {
            OrderPartPage orderPartPage = new OrderPartPage(controller.Stock.PurchaseOrderPartHandler.CurrentPurchase);

            // Create the BikeStockPage instance
            Current.MainWindow.Content = orderPartPage;

            // Define callback
            orderPartPage.ButtonBackCallback = GoToPiecesStockPage;

            orderPartPage.RefreshPartGrid();
        }

        public void GoToAddBikeToStockPage() {
            AddBikeStockPage addBikeStockPage = new AddBikeStockPage(controller.BikeInStock.getBikeStock);

            // Create the AddBikeStockPage instance
            Current.MainWindow.Content = addBikeStockPage;

            // Define callback
            addBikeStockPage.GoBackToChooseCallback = GoToBikeStockPage;

            addBikeStockPage.AddBikeToStockCallback = AddBikeToDataBase;
        }

        public void LoginButtonCallback() {
            if (Current.MainWindow.Content is not LoginPage)
                throw new FormatException("The current page is not a LoginPage");
                
            LoginPage loginPage = Current.MainWindow.Content as LoginPage;

            string username = loginPage.GetUsername();
            string password = loginPage.GetPassword();

            if (controller.Login.CheckUser(username, password))
                GoToChoosePathWindow();
            else
                MessageBox.Show("Wrong username or password");

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
                BikeModelPage bikeModelPage = (Current.MainWindow.Content as BikeModelPage);

                // Get information from the display
                Color colorBike = bikeModelPage.GetColor();
                Size sizeBike = bikeModelPage.GetSize();
                int quantity = bikeModelPage.GetQuantity();

                // Get the bike selected
                Bike currentBikeModel = controller.Catalog.GetSelectedBike(colorBike, sizeBike);

                // Add the bike to the order list
                controller.Order.AddBikeByQuantity(new Bike(currentBikeModel), quantity);

                // Go to the Order Window
                GoToRegistrationCustomerWindow();
            }
            catch (FormatException) {
                MessageBox.Show("The quantity must be an integer !");
            }
            catch (BikeNotFoundException e) {
                MessageBox.Show(e.Message);
            }
        }

        public void AddBikeToDataBase()
        {
            try
            {
                AddBikeStockPage addBikeStockPage = (Current.MainWindow.Content as AddBikeStockPage);

                // Get information from the display
                int quantity = addBikeStockPage.GetQuantityToAdd();
                int modelId = addBikeStockPage.GetModelToAdd();


                // Add the bike number to the stock in DB
                MySqlDataReader reader = controller.DataBase.AddBikeInStock(modelId,quantity);
                reader.Close();

                // Go to the BikeStock page
                GoToBikeStockPage();
            }
            catch (FormatException)
            {
                MessageBox.Show("The quantity must be an integer !");
            }
            catch (BikeNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void CancelOrderCallback() {
            controller.Order.Clear();
            GoToCatalogWindow();
        }

        public void SaveOrderCallback() {
            if (Current.MainWindow.Content is not CustomerRegistrationPage) {
                MessageBox.Show("This callback is for the order validation window !");
                return;
            }

            CustomerRegistrationPage customerRegistrationPage = (Current.MainWindow.Content as CustomerRegistrationPage);

            if (customerRegistrationPage.AreCustomerInfoValid() == false) {
                MessageBox.Show("You must set all the client informations to save the order !");
                return;
            }

            Customer orderCustomer = new Customer() {
                Name = customerRegistrationPage.GetNameCustomer(),
                Address = customerRegistrationPage.GetAddressCustomer(),
                TVA = customerRegistrationPage.GetTVACustomer(),
                Phone = customerRegistrationPage.GetPhoneCustomer()
            };

            controller.SaveOrderInformation(orderCustomer);

            GoToOrderConfirmationWindow();
        }

        void ValidateOrderCallback() {
            controller.SaveOrderAndSlotInDatabase();
            controller.Order.Clear();
            GoToCatalogWindow();
        }

        void GoToOrderListCallback() {
            controller.RefreshOrderAndPlanningFromDatabase();
            GoToOrderListPage();
        }

        void SavePlanningCallback() {
            if (Current.MainWindow.Content is not PlanningPage) {
                MessageBox.Show("Callback only use by the PlanningPage");
                return;
            }

            PlanningPage planningPage = (Current.MainWindow.Content as PlanningPage);
            DateTime now = DateTime.Now;
            List<Order> ordersDisplayed = planningPage.OrderToShow;
            controller.UpdatePlanningAfterUserUpdate(ordersDisplayed);
            TimeSpan timespan = DateTime.Now - now;
            Trace.WriteLine(timespan.TotalMilliseconds);

            GoToPlanningPage();
        }

        void SetSlotForEveryPlanningDate() {
            if (Current.MainWindow.Content is not PlanningPage) {
                MessageBox.Show("Callback only use by the PlanningPage");
                return;
            }

            PlanningPage planningPage = Current.MainWindow.Content as PlanningPage;

            foreach (PlanningRow currentPlanningRow in planningPage.PlanningRows) {
                DateTime deliveryDate = currentPlanningRow.DeliveryDate;
                List<Slot> freeSlotFromDate = controller.Planning.GetFreeSlotFromDate(deliveryDate);
                currentPlanningRow.SlotAvailable = freeSlotFromDate.Select(s => s.SlotNumber).ToList();
            }
        }

        void SetSlotForBikeInPlanningPage(DateTime dateSelected) {
            if (Current.MainWindow.Content is not PlanningPage) {
                MessageBox.Show("Callback only use by the PlanningPage");
                return;
            }

            PlanningPage planningPage = Current.MainWindow.Content as PlanningPage;
            PlanningRow selectedPlanningRow = planningPage.GetCurrentPlanningRow();
            List<Slot> freeSlotFromDate = controller.Planning.GetFreeSlotFromDate(dateSelected);
            selectedPlanningRow.SlotAvailable = freeSlotFromDate.Select(s => s.SlotNumber).ToList();
        }

        void SlotChangedOnPlanningPage(int slotNumber) {
            if (Current.MainWindow.Content is not PlanningPage) {
                MessageBox.Show("Callback only use by the PlanningPage");
                return;
            }

            PlanningPage planningPage = Current.MainWindow.Content as PlanningPage;
            PlanningRow planningRow = planningPage.GetCurrentPlanningRow();

            if (planningRow is not null) {
                controller.BindBikeToNewSLot(planningRow.IdOrder, planningRow.IdOrderModel, planningRow.DeliveryDate, slotNumber);

                planningPage.ShowPlanning();
            }
        }

        void GoToStockPageCallback() {
            controller.Stock.GetStockFromDataBase();
            controller.Stock.GetPartPerBikeFromDatabase();
            GoToPiecesStockPage();
        }

        void GoToOrderPartPageCallback() {
            controller.RefreshOrderAndPlanningFromDatabase();
            controller.SetPartsToOrder();
            GoToOrderPartPage();
        }
    }
}
