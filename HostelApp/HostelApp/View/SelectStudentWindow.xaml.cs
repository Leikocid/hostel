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
using System.Windows.Shapes;

namespace HostelApp.View
{
    /// <summary>
    /// Interaction logic for SelectStudentWindow.xaml
    /// </summary>
    public partial class SelectStudentWindow : Window
    {
        public int SelectedId = -1;

        public SelectStudentWindow()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grid = (frmSelection.Content as StudentsPage).grdStudents;
            if (grid.SelectedIndex >=0) {
                SelectedId = (grid.Items[grid.SelectedIndex] as StudentsPage.StudentRecord).Id;
                Close();
            }
        }
    }
}
