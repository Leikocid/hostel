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
    /// Interaction logic for RoomPage.xaml
    /// </summary>
    public partial class RoomPage : Page
    {

        public RoomPage()
        {
            InitializeComponent();
        }

        private Room room;
        public Room Room
        {
            get
            {
                return this.room;
            }
            set
            {
                this.room = value;
                InitializeForm();
            }
        }

        public class Record
        {
            public int Id { set; get; }
            public String Фамилия { set; get; }
            public String Имя { set; get; }
            public String Отчество { set; get; }
            public String Факультет { set; get; }
            public int Курс { set; get; }
            public int Группа { set; get; }
        }

        public void InitializeForm() {
            txtRoomNum.Text = room.Number.ToString();
            txtHostelNum.Text = room.Hostel.Name;
            txtRoomPlaces.Text = room.Capacity.ToString();

            using (var context = new HostelModelContainer())
            {
                var query = context.OcupationSet.Where(o => o.Room.Id == room.Id && o.Student.Active == true).Select(o => o.Student).ToList().Select(s => new Record
                {
                    Id = s.Id,
                    Фамилия = s.Person.LastName,
                    Имя = s.Person.FirstName,
                    Отчество = s.Person.MiddleName,
                    Факультет = s.Group.Faculty.Name,
                    Курс = s.Group.StudyYear,
                    Группа = s.Group.Number
                });
                grdStudents.ItemsSource = query.ToList();
            }
            txtFreePlace.Text = (room.Capacity - grdStudents.Items.Count).ToString();
        }
    }
}
