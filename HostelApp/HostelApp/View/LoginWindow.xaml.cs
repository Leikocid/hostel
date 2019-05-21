using HostelApp.Model;
using HostelApp.Service;
using System.Windows;
using System.Windows.Input;

namespace HostelApp.View {
    /// <summary>
    /// Логика взаимодействия для LoginScreen.xaml
    /// </summary>
    public partial class LoginWindow : Window {
        public LoginWindow() {
            InitializeComponent();
            txtUsername.Focus();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e) {
            Login();
        }

        private void txtUsername_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                Login();
            }
        }

        public void Login() {
            User user = DataService.Login(txtUsername.Text, txtPassword.Password);
            if (user == null) {
                lblError.Visibility = Visibility.Visible;
            } else {
                MainWindow.currentUser = user;
                this.Close();
            }
        }
    }
}
