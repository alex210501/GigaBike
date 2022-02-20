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
using MySql.Data.MySqlClient;

namespace GigaBike {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            DataBase db = new DataBase();
            db.Connect();

            MySqlDataReader rdr = db.GetModels();

            while (rdr.Read()) {
                Trace.WriteLine(string.Format("{0} {1} {2} {3} {4}", rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetString(4)));
            }
        }
    }
}
