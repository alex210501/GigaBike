﻿using System;
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

namespace GigaBike {
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page {
        static Action loginButtonCallback = null;

        public LoginPage() {
            InitializeComponent();
        }

        private void ButtonLogin(object sender, RoutedEventArgs e) {
            if (loginButtonCallback != null) loginButtonCallback();
        }

        public Action LoginButtonCallback {
            set {
                loginButtonCallback = value;
            }
        }

        public string GetUsername() {
            return UsernameInput.Text;
        }

        public string GetPassword() {
            return PasswordInput.Password;
        }
    }
}
