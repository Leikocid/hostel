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
    /// Interaction logic for OcupationPage.xaml
    /// </summary>
    public partial class OcupationPage : Page
    {

        List<Hostel> hostels = new List<Hostel>();
        
        public OcupationPage()
        {
            InitializeComponent();
            InitFilters();
            InitializeRooms();
        }

        public void InitFilters()
        {
            hostels.Clear();
            cmbHostel.Items.Clear();
            using (var context = new HostelModelContainer())
            {
                hostels = context.HostelSet.ToList();
                foreach (Hostel hostel in hostels)
                {
                    cmbHostel.Items.Add(hostel.Name + " (" + hostel.Address + ")");
                }
            }

        }

        private void CmbHostel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeRooms();
        }

        public class RoomRecord
        {
            public int Id { set; get; }
            public String Общежитие { set; get; }
            public int Этаж { set; get; }
            public int Номер { set; get; }
            public int Вместимость { set; get; }
            public int Заселено { set; get; }
            public int Свободно { set; get; }
        }

        public void InitializeRooms()
        {

            using (var context = new HostelModelContainer())
            {
                if (cmbHostel.SelectedIndex == -1)
                {
                    var query = from r in context.RoomSet
                                select new RoomRecord {
                                    Id = r.Id,
                                    Общежитие = r.Hostel.Name + "(" + r.Hostel.Address + ")",
                                    Этаж = r.Floor,
                                    Номер = r.Number,
                                    Вместимость = r.Capacity,
                                    Заселено = (from o in context.OcupationSet
                                                      where o.Room == r &&
                                                          o.Active &&
                                                          o.Student.Active &&
                                                          o.FromDate < DateTime.Now &&
                                                          (o.ToDate > DateTime.Now || o.ToDate == null)
                                                      select o).Count()
                                };
                    List<RoomRecord> records = query.ToList();
                    records.ForEach(r => r.Свободно = r.Вместимость - r.Заселено);
                    records.Sort((r1, r2) => r2.Свободно.CompareTo(r1.Свободно));
                    grdRooms.ItemsSource = records; 
                }
                else
                {
                    int hostelId = hostels[cmbHostel.SelectedIndex].Id;
                    var query = from r in context.RoomSet
                                where r.Hostel.Id == hostelId
                                select new RoomRecord
                                {
                                    Id = r.Id,
                                    Общежитие = r.Hostel.Name + " (" + r.Hostel.Address + ")",
                                    Этаж = r.Floor,
                                    Номер = r.Number,
                                    Вместимость = r.Capacity,
                                    Заселено = (from o in context.OcupationSet
                                                where o.Room == r&&
                                                          o.Active &&
                                                          o.Student.Active &&
                                                          o.FromDate < DateTime.Now &&
                                                          (o.ToDate > DateTime.Now || o.ToDate == null)
                                                select o).Count()
                                };
                    List<RoomRecord> records = query.ToList();
                    records.ForEach(r => r.Свободно = r.Вместимость - r.Заселено);
                    records.Sort((r1, r2) => r2.Свободно.CompareTo(r1.Свободно));
                    grdRooms.ItemsSource = records;
                }
            }
        }

        public class StudentRecord
        {
            public int Id { set; get; }
            public String Фамилия { set; get; }
            public String Имя { set; get; }
            public String Отчество { set; get; }
            public String Факультет { set; get; }
            public int Курс { set; get; }
            public int Группа { set; get; }
            public DateTime From { set; get; }
            public DateTime? To { set; get; }
            public String ДатаС { set; get; }
            public String ДатаПо { set; get; }
        }

        List<StudentRecord> students = new List<StudentRecord>();

        private void GrdRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStudentsGrid();
        }

        private void UpdateStudentsGrid() {
            students.Clear();
            grdStudents.ItemsSource = students;

            int selectedIndex = grdRooms.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (grdRooms.Items[selectedIndex] is RoomRecord roomRecord && roomRecord.Заселено > 0)
                {
                    using (var context = new HostelModelContainer())
                    {
                        var query = from o in context.OcupationSet
                                    where o.Room.Id == roomRecord.Id &&
                                        o.Active &&
                                        o.Student.Active &&
                                        o.FromDate < DateTime.Now &&
                                        (o.ToDate > DateTime.Now || o.ToDate == null)
                                    select new StudentRecord
                                    {
                                        Id = o.Id,
                                        Фамилия = o.Student.Person.LastName,
                                        Имя = o.Student.Person.FirstName,
                                        Отчество = o.Student.Person.MiddleName,
                                        Факультет = o.Student.Group.Faculty.Name,
                                        Курс = o.Student.Group.StudyYear,
                                        Группа = o.Student.Group.Number,
                                        From = o.FromDate,
                                        To = o.ToDate
                                    };
                        List<StudentRecord> students = query.ToList();
                        students.ForEach(s => { s.ДатаС = s.From.ToString("dd.MM.yyyy"); s.ДатаПо = s.To?.ToString("dd.MM.yyyy"); });
                        students.Sort((r1, r2) => r2.ДатаС.CompareTo(r1.ДатаС));
                        grdStudents.ItemsSource = students;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = grdRooms.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (grdRooms.Items[selectedIndex] is RoomRecord roomRecord && roomRecord.Свободно > 0)
                {
                    // вызов селектора студентов
                    SelectStudentWindow selectStudentWindow = new SelectStudentWindow();
                    selectStudentWindow.lblHeader.Content = "Заселение в: " + roomRecord.Общежитие + " комната " + roomRecord.Номер;
                    selectStudentWindow.lblHeader.Visibility = Visibility.Visible;
                    selectStudentWindow.ShowDialog();
                    selectStudentWindow.Close();
                    
                    if (selectStudentWindow.SelectedId >=0) {
                        // заселение или переселение студета, если он уже живет. 
                        // Студент: selectStudentWindow.SelectedId, Комната: roomRecord.Id, fromDate = Today, toDate = null, order = null
                        using (var context = new HostelModelContainer())
                        {
                            Ocupation previousOcupation = context.OcupationSet.SingleOrDefault(o => o.Student.Id == selectStudentWindow.SelectedId && o.Active == true);                           
                            if (previousOcupation == null || (previousOcupation.Room.Id != roomRecord.Id)) {
                                if (previousOcupation != null)
                                {
                                    previousOcupation.Active = false;
                                }
                                Ocupation ocupation = new Ocupation()
                                {
                                    Active = true,
                                    Student = context.StudentSet.Single(s => s.Id == selectStudentWindow.SelectedId),
                                    Room = context.RoomSet.Single(r => r.Id == roomRecord.Id),
                                    FromDate = DateTime.Today
                                };
                                context.OcupationSet.Add(ocupation);
                                context.SaveChanges();

                                // обновление UI
                                List<RoomRecord> records = new List<RoomRecord>();
                                foreach (RoomRecord r in grdRooms.ItemsSource) {
                                    records.Add(r);
                                    if (previousOcupation!=null && r.Id == previousOcupation.Room.Id) {
                                        r.Заселено = r.Заселено - 1;
                                        r.Свободно = r.Свободно + 1;
                                    }
                                } 
                                records[selectedIndex].Заселено = records[selectedIndex].Заселено + 1;
                                records[selectedIndex].Свободно = records[selectedIndex].Свободно - 1;
                                grdRooms.ItemsSource = records;                                   
                                UpdateStudentsGrid();
                            }                        
                        }
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int selectedIndex = grdRooms.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (grdRooms.Items[selectedIndex] is RoomRecord roomRecord && roomRecord.Свободно > 0)
                {
                    int studentIndex = grdStudents.SelectedIndex;
                    if (studentIndex >= 0)
                    {
                        if (grdStudents.Items[studentIndex] is StudentRecord studentRecord)
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Вы уверены что хотите выселить студента?", "Подтверждение действия", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                using (var context = new HostelModelContainer())
                                {
                                    Ocupation previousOcupation = context.OcupationSet.SingleOrDefault(o => o.Id == studentRecord.Id && o.Active == true);
                                    previousOcupation.Active = false;
                                    context.SaveChanges();

                                    // обновление UI
                                    List<RoomRecord> records = new List<RoomRecord>();
                                    foreach (RoomRecord r in grdRooms.ItemsSource)
                                    {
                                        records.Add(r);
                                        if (r.Id == previousOcupation.Room.Id)
                                        {
                                            r.Заселено = r.Заселено - 1;
                                            r.Свободно = r.Свободно + 1;
                                        }
                                    }
                                    grdRooms.ItemsSource = records;
                                    UpdateStudentsGrid();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
