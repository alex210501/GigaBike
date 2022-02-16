using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GigaBike {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
		protected override  void OnStartup(StartupEventArgs e) {
            Current.MainWindow = new LoginWindow();
            
            if (Current.MainWindow is LoginWindow)
                (Current.MainWindow as LoginWindow).LoginButtonCallback = GoToChoosePathWindow;
            Current.MainWindow.Show();
        }

        public void GoToChoosePathWindow() {
            Current.MainWindow.Hide();
            Current.MainWindow = new ChoosePathWindow();
            Current.MainWindow.Show();
        }

        public void GoToCatalogWindow() {
            Current.MainWindow.Hide();
            Current.MainWindow = new CatalogWindow();
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
    }
}
