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

        RoomsPage roomsPage;
        SummaryPage summaryPage;
        StudentsPage studentsPage;

        public MainWindow()
        {
            InitializeComponent();

            summaryPage = new SummaryPage();
            roomsPage = new RoomsPage();
            studentsPage = new StudentsPage();

            frameMain.Content = summaryPage;
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
    }
}
