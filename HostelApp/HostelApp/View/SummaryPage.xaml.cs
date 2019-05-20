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
    /// Interaction logic for SummaryView.xaml
    /// </summary>
    public partial class SummaryPage: Page
    {
        public SummaryPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new HostelModelContainer())
            {
                int total = context.RoomSet.Sum(r => r.Capacity);
                lblTotal.Content = total.ToString();

                int occupied = context.OccupationSet.Where(o => 
                    o.Active && 
                    o.Student.Active && 
                    o.FromDate < DateTime.Now && 
                    (o.ToDate > DateTime.Now || o.ToDate == null)
                ).Count();
                lblFree.Content = (total - occupied).ToString();

                double orders = context.OrderSet.Sum(o => (double?)o.Price) ?? 0;
                double billed = context.PaymentSet.Sum(p => (double?)p.Amount) ?? 0;
                lblDept.Content = (orders - billed).ToString();
            }
        }
    }
}
