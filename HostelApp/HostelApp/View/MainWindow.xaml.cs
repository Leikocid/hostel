using HostelApp.Model;
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

namespace HostelApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // текущий залогиненный пользователь
        public static User currentUser;

        RoomsPage roomsPage;
        SummaryPage summaryPage;
        StudentsPage studentsPage;
        OcupationPage ocupationPage;
        AddEditStudentPage addEditStudentPage;

        public MainWindow()
        {
            InitializeComponent();

            summaryPage = new SummaryPage();
            roomsPage = new RoomsPage();
            studentsPage = new StudentsPage();
            ocupationPage = new OcupationPage();
            addEditStudentPage = new AddEditStudentPage();

            frameMain.Content = summaryPage;

            UpdateUserUIAccess();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Content = roomsPage;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            frameMain.Content = studentsPage;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            frameMain.Content = summaryPage;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            frameMain.Content = ocupationPage;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            frameMain.Content = addEditStudentPage;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            loginWindow.Close();
            UpdateUserUIAccess();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            currentUser = null;
            UpdateUserUIAccess();
        }

        public void UpdateUserUIAccess() {
            if (currentUser == null)
            {
                lblWelcomeName.Content = "гость";
                summaryPage.pnlAdmin.Visibility = Visibility.Hidden;
                btnLogout.Visibility = Visibility.Collapsed;
                btnLogin.Visibility = Visibility.Visible;
                if (frameMain.Content!= null && 
                    !frameMain.Content.Equals(studentsPage) && !frameMain.Content.Equals(roomsPage) && !frameMain.Content.Equals(summaryPage)) {
                    frameMain.Content = summaryPage;
                }
                pnlAdmin.Visibility = Visibility.Hidden;
            }
            else {
                if (currentUser.Person == null) {
                    lblWelcomeName.Content = "анонимный администратор";
                } else {
                    lblWelcomeName.Content = currentUser.Person.FirstName + " " + currentUser.Person.MiddleName;
                }
                summaryPage.pnlAdmin.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Visible;
                btnLogin.Visibility = Visibility.Collapsed;
                pnlAdmin.Visibility = Visibility.Visible;
            }
        }
     }
}
