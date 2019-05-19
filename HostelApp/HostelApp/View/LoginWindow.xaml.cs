using HostelApp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace HostelApp.View
{
    /// <summary>
    /// Логика взаимодействия для LoginScreen.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HostelModelContainer())
            {
                User user = context.UserSet.Where(u => u.Login == txtUsername.Text && u.Pasword == txtPassword.Password && u.Active == true).FirstOrDefault();
                if (user == null)
                {
                    lblError.Visibility = Visibility.Visible;
                }
                else
                {
                    MainWindow.currentUser = user;
                    Person person = user.Person; // загрузить дополнительные данные в объект заранее
                    this.Close();
                }
            }
        }
    }
}
