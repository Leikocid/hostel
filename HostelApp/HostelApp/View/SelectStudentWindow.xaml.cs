using HostelApp.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace HostelApp.View {
    /// <summary>
    /// Interaction logic for SelectStudentWindow.xaml
    /// </summary>
    public partial class SelectStudentWindow : Window {
        public int SelectedId = -1;

        public SelectStudentWindow() {
            InitializeComponent();
         }

         private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e) {
            SelectStudent();
        }

        public void SelectStudent() {
            DataGrid grid = (frmSelection.Content as StudentsPage).grdStudents;
            if (grid.SelectedIndex >= 0) {
                SelectedId = (grid.Items[grid.SelectedIndex] as DataService.StudentRecord).Id;
                Close();
            }
        }

         private void GrdStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            SelectStudent();
        }

        private void FrmSelection_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e) {
            DataGrid grid = (frmSelection.Content as StudentsPage).grdStudents;
            grid.MouseDoubleClick += GrdStudents_MouseDoubleClick;
        }
    }
}
