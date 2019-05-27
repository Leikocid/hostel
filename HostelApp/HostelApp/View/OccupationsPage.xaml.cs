using HostelApp.Model;
using HostelApp.Service;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HostelApp.View {
    /// <summary>
    /// Interaction logic for OcupationPage.xaml
    /// </summary>
    public partial class OccupationsPage : Page {

        List<Hostel> hostels = new List<Hostel>();

        public OccupationsPage() {
            InitializeComponent();
            InitFilters();
            InitializeRooms();
        }

        public void InitFilters() {
            hostels.Clear();
            cmbHostel.Items.Clear();
            hostels = DataService.GetAllHostels();
            foreach (Hostel hostel in hostels) {
                cmbHostel.Items.Add(hostel.Name + " (" + hostel.Address + ")");
            }
        }

        private void CmbHostel_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            InitializeRooms();
        }

        public void InitializeRooms() {
            int hostelId = -1;
            if (cmbHostel.SelectedIndex >= 0) {
                hostelId = hostels[cmbHostel.SelectedIndex].Id;
            }
            List<DataService.RoomRecord> records = DataService.GetRooms(hostelId);
            records.ForEach(r => r.Free = r.Capacity - r.Occupied);
            records.Sort((r1, r2) => r2.Free.CompareTo(r1.Free));
            grdRooms.ItemsSource = records;
        }

        List<DataService.StudentRecord> students = new List<DataService.StudentRecord>();

        private void GrdRooms_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateStudentsGrid();
        }

        private void UpdateStudentsGrid() {
            students.Clear();
            grdStudents.ItemsSource = students;
            int selectedIndex = grdRooms.SelectedIndex;
            if (selectedIndex >= 0) {
                if (grdRooms.Items[selectedIndex] is DataService.RoomRecord roomRecord && roomRecord.Occupied > 0) {
                    List<DataService.StudentRecord> students = DataService.FindStudents(roomRecord.Id, true);
                    students.ForEach(s => { s.StudentOccupation.FromText = s.StudentOccupation.From.ToString("dd.MM.yyyy"); s.StudentOccupation.ToText = s.StudentOccupation.To?.ToString("dd.MM.yyyy"); });
                    students.Sort((r1, r2) => r2.StudentOccupation.From.CompareTo(r1.StudentOccupation.From));
                    grdStudents.ItemsSource = students;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            int selectedIndex = grdRooms.SelectedIndex;
            if (selectedIndex >= 0) {
                if (grdRooms.Items[selectedIndex] is DataService.RoomRecord roomRecord && roomRecord.Free > 0) {
                    // вызов селектора студентов
                    SelectStudentWindow selectStudentWindow = new SelectStudentWindow();
                    selectStudentWindow.lblHeader.Content = "Заселение в: " + roomRecord.Hostel + " комната " + roomRecord.Number;
                    selectStudentWindow.lblHeader.Visibility = Visibility.Visible;
                    selectStudentWindow.ShowDialog();
                    selectStudentWindow.Close();

                    if (selectStudentWindow.SelectedId >= 0) {
                        (Occupation previousOccupation, Occupation occupation) = DataService.SetOccupation(selectStudentWindow.SelectedId, roomRecord.Id);

                        // обновление UI
                        List<DataService.RoomRecord> records = new List<DataService.RoomRecord>();
                        foreach (DataService.RoomRecord r in grdRooms.ItemsSource) {
                            records.Add(r);
                            if (previousOccupation != null && r.Id == previousOccupation.Room.Id) {
                                r.Occupied = r.Occupied - 1;
                                r.Free = r.Free + 1;
                            }
                        }
                        records[selectedIndex].Occupied = records[selectedIndex].Occupied + 1;
                        records[selectedIndex].Free = records[selectedIndex].Free - 1;
                        grdRooms.ItemsSource = records;
                        UpdateStudentsGrid();
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            int selectedIndex = grdRooms.SelectedIndex;
            if (selectedIndex >= 0) {
                if (grdRooms.Items[selectedIndex] is DataService.RoomRecord roomRecord && roomRecord.Free > 0) {
                    int studentIndex = grdStudents.SelectedIndex;
                    if (studentIndex >= 0) {
                        if (grdStudents.Items[studentIndex] is DataService.StudentRecord studentRecord) {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Вы уверены что хотите выселить студента?", "Подтверждение действия", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes) {
                                (Occupation previousOccupation, Occupation occupation) = DataService.SetOccupation(studentRecord.StudentOccupation.Id, -1);
  
                                // обновление UI
                                List<DataService.RoomRecord> records = new List<DataService.RoomRecord>();
                                foreach (DataService.RoomRecord r in grdRooms.ItemsSource) {
                                    records.Add(r);
                                    if (previousOccupation != null && r.Id == previousOccupation.Room.Id) {
                                        r.Occupied = r.Occupied - 1;
                                        r.Free = r.Free + 1;
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

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            int selectedIndex = grdRooms.SelectedIndex;
            if (selectedIndex >= 0) {
                if (grdRooms.Items[selectedIndex] is DataService.RoomRecord roomRecord && roomRecord.Free > 0) {
                    int studentIndex = grdStudents.SelectedIndex;
                    if (studentIndex >= 0) {
                        if (grdStudents.Items[studentIndex] is DataService.StudentRecord studentRecord) {
                            EditOccupationWindow editOccupationWindow = new EditOccupationWindow {
                                OccupationId = studentRecord.StudentOccupation.Id
                            };
                            editOccupationWindow.ShowDialog();
                            UpdateStudentsGrid();
                        }
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) {
            int selectedIndex = grdRooms.SelectedIndex;
            if (selectedIndex >= 0) {
                if (grdRooms.Items[selectedIndex] is DataService.RoomRecord roomRecord && roomRecord.Free > 0) {
                    int studentIndex = grdStudents.SelectedIndex;
                    if (studentIndex >= 0) {
                        if (grdStudents.Items[studentIndex] is DataService.StudentRecord studentRecord && studentRecord.StudentOccupation.Price > 0) {
                            AddPaymentWindow addPaymentWindow = new AddPaymentWindow {
                                OccupationId = studentRecord.StudentOccupation.Id
                            };
                            addPaymentWindow.ShowDialog();
                            UpdateStudentsGrid();
                        }
                    }
                }
            }
        }
    }
}
