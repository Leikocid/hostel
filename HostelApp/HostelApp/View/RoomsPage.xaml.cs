using HostelApp.Model;
using HostelApp.Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HostelApp.View {
    /// <summary>
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsPage : Page {
        List<Hostel> hostels = new List<Hostel>();
        RoomPage roomPage;

        public RoomsPage() {
            InitializeComponent();
            roomPage = new RoomPage();
            InitFilters();
        }

        public void InitFilters() {
            hostels.Clear();
            cmbHostel.Items.Clear();
            hostels = DataService.GetAllHostels();
            foreach (Hostel hostel in hostels) {
                cmbHostel.Items.Add(hostel.Name + " (" + hostel.Address + ")");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Search();
        }

        private void TbxRoom_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Return) {
               Search();
            }
        }

        private void Search() {
            var isNumeric = int.TryParse(tbxRoom.Text, out int roomNumber);
            if (cmbHostel.SelectedIndex < 0 || !isNumeric) {
                lblNotFound.Visibility = Visibility.Visible;
                frameRoom.Visibility = Visibility.Collapsed;
            } else {
                int hostelId = hostels[cmbHostel.SelectedIndex].Id;
                Room room = DataService.FindRoom(hostelId, roomNumber);
                if (room == null) {
                    lblNotFound.Visibility = Visibility.Visible;
                    frameRoom.Visibility = Visibility.Collapsed;
                } else {
                    roomPage.RoomId = room.Id;
                    lblNotFound.Visibility = Visibility.Collapsed;
                    frameRoom.Content = roomPage;
                    frameRoom.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
