using HostelApp.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HostelApp.View {
    /// <summary>
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page {

        public StudentsPage() {
            InitializeComponent();
            tbxSearchText.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            FindStudents();
        }

        private void TbxSearchText_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                FindStudents();
            }
        }

        private void FindStudents() {
            grdStudents.ItemsSource = DataService.FindStudents(tbxSearchText.Text);
        }
    }
}
