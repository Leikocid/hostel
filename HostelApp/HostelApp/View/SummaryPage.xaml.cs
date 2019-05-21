using HostelApp.Service;
using System.Windows;
using System.Windows.Controls;

namespace HostelApp.View {
    /// <summary>
    /// Interaction logic for SummaryView.xaml
    /// </summary>
    public partial class SummaryPage : Page {

        public SummaryPage() {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            int total = DataService.GetRoomsCount();
            lblTotal.Content = total.ToString();

            int occupied = DataService.GetOccupiedRoomsCount();
            lblFree.Content = (total - occupied).ToString();

            double orders = DataService.GetOrdersSum();
            double billed = DataService.GetPaymentsSum();
            lblDept.Content = (orders - billed).ToString();
        }
    }
}
