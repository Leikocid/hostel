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
                                    Общежитие = r.Hostel.Name + "(" + r.Hostel.Address + ")",
                                    Этаж = r.Floor,
                                    Номер = r.Number,
                                    Вместимость = r.Capacity,
                                    Заселено = (from o in context.OcupationSet
                                                where o.Room == r&&
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
            public DateTime ДатаС { set; get; }
            public DateTime? ДатаПо { set; get; }
        }

        private void GrdRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = grdRooms.SelectedIndex;
            if (selectedIndex > 0)
            {
                RoomRecord roomRecord = grdRooms.Items[selectedIndex] as RoomRecord;
                if (roomRecord != null && roomRecord.Заселено > 0)
                {
                    using (var context = new HostelModelContainer())
                    {
                        var query = from o in context.OcupationSet
                                    where o.Room.Id == roomRecord.Id &&
                                        o.Student.Active &&
                                        o.FromDate > DateTime.Now &&
                                        (o.ToDate < DateTime.Now || o.ToDate == null)
                                    select new StudentRecord
                                    {
                                        Id = o.Id,
                                        Фамилия = o.Student.Person.LastName,
                                        Имя = o.Student.Person.FirstName,
                                        Отчество = o.Student.Person.MiddleName,
                                        Факультет = o.Student.Group.Faculty.Name,
                                        Курс = o.Student.Group.StudyYear,
                                        Группа = o.Student.Group.Number,
                                        ДатаС = o.FromDate,
                                        ДатаПо = o.ToDate
                                    };
                        List<StudentRecord> records = query.ToList();
                        records.Sort((r1, r2) => r2.ДатаС.CompareTo(r1.ДатаС));
                        grdStudents.ItemsSource = records;
                    }
                }
            }
        }
    }
}
