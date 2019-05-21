using HostelApp.Model;
using HostelApp.Service;
using System.Windows.Controls;

namespace HostelApp.View {
    /// <summary>
    /// Interaction logic for RoomPage.xaml
    /// </summary>
    public partial class RoomPage : Page {

        public RoomPage() {
            InitializeComponent();
        }

        private int roomId;
        public int RoomId {
            get {
                return this.roomId;
            }
            set {
                this.roomId = value;
                InitializeForm();
            }
        }

        public void InitializeForm() {
            Room room = DataService.LoadRoom(RoomId);
            txtRoomNum.Text = room.Number.ToString();
            txtHostelNum.Text = room.Hostel.Name;
            txtRoomPlaces.Text = room.Capacity.ToString();
            grdStudents.ItemsSource = DataService.FindStudents(roomId, false);
            txtFreePlace.Text = (room.Capacity - grdStudents.Items.Count).ToString();
        }
    }
}
