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
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        List<Hostel> hostels = new List<Hostel>();
        RoomPage roomPage;

        public RoomsPage()
        {
            InitializeComponent();
            roomPage = new RoomPage();
            InitFilters();
        }

        public void InitFilters() {
            hostels.Clear();
            cmbHostel.Items.Clear();
            using (var context = new HostelModelContainer())
            {
                hostels = context.HostelSet.ToList();
                foreach (Hostel hostel in hostels) {
                    cmbHostel.Items.Add(hostel.Name + " (" + hostel.Address + ")" );
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var isNumeric = int.TryParse(tbxRoom.Text, out int roomNumber);
            if (cmbHostel.SelectedIndex < 0 || !isNumeric)
            {
                lblNotFound.Visibility = Visibility.Visible;
                frameRoom.Visibility = Visibility.Collapsed;
            } else {
                using (var context = new HostelModelContainer())
                {
                    int hostelId = hostels[cmbHostel.SelectedIndex].Id;
                    Room room = context.RoomSet.Where(r => r.Number == roomNumber && r.Hostel.Id == hostelId).FirstOrDefault();
                    if (room == null) {
                        lblNotFound.Visibility = Visibility.Visible;
                        frameRoom.Visibility = Visibility.Collapsed;
                    } else {
                        roomPage.Room = room;
                        lblNotFound.Visibility = Visibility.Collapsed;
                        frameRoom.Content = roomPage;
                        frameRoom.Visibility = Visibility.Visible;
                    }
                }
            }
        }
    }
}
