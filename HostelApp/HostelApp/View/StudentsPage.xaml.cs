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
using HostelApp.Model;
using System.Data.Entity;
using System.Data;

namespace HostelApp.View
{
    /// <summary>
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {

        public StudentsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            findStudents();
        }

        private void TbxSearchText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) {
                findStudents();
            }
        }

        public class Record {
            public int Id { set; get; }
            public String Фамилия { set; get; }
            public String Имя { set; get; }
            public String Отчество { set; get; }
            public String Факультет { set; get; }
            public int Курс { set; get; }
            public int Группа { set; get; }
        }

        private void findStudents() {
            using (var context = new HostelModelContainer())
            {
                var query = context.StudentSet
                                   .Where(s => s.Person.FirstName.ToLower().StartsWith(tbxSearchText.Text.ToLower()) || s.Person.SecondName.ToLower().StartsWith(tbxSearchText.Text.ToLower()))
                                   .Take<Student>(10).Select(s => new Record
                                   {
                                       Id = s.Id,
                                       Фамилия = s.Person.SecondName,
                                       Имя = s.Person.FirstName,
                                       Отчество = s.Person.MiddleName,
                                       Факультет = s.Group.Faculty.Name,
                                       Курс = s.Group.StudyYear,
                                       Группа = s.Group.Number });
                query.ToList();
                grdStudents.ItemsSource = query.ToList();
                if (grdStudents.Columns.Count > 0)
                {
                   // grdStudents.Columns[0].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void GrdStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editStudent();
        }

        private void editStudent(){
             MessageBox.Show("" + ((grdStudents.SelectedItem as Record).Id));
        }
    }
}
