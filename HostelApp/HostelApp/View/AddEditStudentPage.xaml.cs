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
    /// Interaction logic for AddEditStudentPage.xaml
    /// </summary>
    public partial class AddEditStudentPage : Page
    {
        public AddEditStudentPage()
        {
            InitializeComponent();
            InitFilters();
            SwitchToNewMode();
        }

        List<Faculty> faculties = new List<Faculty>();

        private void InitFilters()
        {
            faculties.Clear();
            cmbFaculty.Items.Clear();
            using (var context = new HostelModelContainer())
            {
                faculties = context.FacultySet.ToList();
                foreach (Faculty faculty in faculties)
                {
                    cmbFaculty.Items.Add(faculty.Name);
                }
            }
        }

        // объект для редактирования
        private Student currentStudent;
        private int currentStudentId = -1;

        public class OccupationOrderRecord
        {
            public double Price { set; get; }
            public String Number { set; get; }
            public double PaymentAmount { set; get; }
        }

        public class OccupationRecord
        {
            public int Id { set; get; }
            public String Hostel { set; get; }
            public int RoomNumber { set; get; }
            public DateTime FromDate { set; get; }
            public String From { set; get; }
            public DateTime? ToDate { set; get; }
            public String To { set; get; }
            public OccupationOrderRecord Order { set; get; }
       }

        public class PaymentOrderRecord {
            public double Price { set; get; }
            public String Number { set; get; }
            public DateTime OrderDate { set; get; }
            public String Date { set; get; }
        }
        public class PaymentRecord
        {
            public int Id { set; get; }
            public double Amount { set; get; }
            public String Number { set; get; }
            public DateTime PaymentDate { set; get; }
            public String Date { set; get; }
            public PaymentOrderRecord Order { set; get; }
        }

        private void SwitchToNewMode()
        {
            currentStudent = null;
            currentStudentId = -1;
            lblMode.Content = "- новый -";
            txtLN.Text = "";
            txtFN.Text = "";
            txtMN.Text = "";
            txtPasp.Text = "";
            txtAddr.Text = "";
            txtFLN.Text = "";
            txtFFN.Text = "";
            txtFMN.Text = "";
            txtMLN.Text = "";
            txtMFN.Text = "";
            txtMMN.Text = "";
            cmbFaculty.SelectedIndex = -1;
            txtYear.Text = "1";
            txtGrpNumber.Text = "";
            grdOHist.ItemsSource = new List<OccupationRecord>();
            grdPHist.ItemsSource = new List<PaymentRecord>();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SwitchToNewMode();
        }

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            // вызов селектора студентов
            SelectStudentWindow selectStudentWindow = new SelectStudentWindow();
            selectStudentWindow.ShowDialog();
            selectStudentWindow.Close();

            if (selectStudentWindow.SelectedId >= 0)
            {
                SwitchToEditMode(selectStudentWindow.SelectedId);               
            }
        }

        private void SwitchToEditMode(int studentId)
        {
            currentStudentId = studentId;
            using (var context = new HostelModelContainer())
            {
                currentStudent = context.StudentSet.Single(s => s.Id == currentStudentId);
                lblMode.Content = currentStudent.Person.LastName + " " + currentStudent.Person.FirstName + " " + currentStudent.Person.MiddleName;
                txtLN.Text = currentStudent.Person.LastName;
                txtFN.Text = currentStudent.Person.FirstName;
                txtMN.Text = currentStudent.Person.MiddleName;
                txtPasp.Text = currentStudent.Person.Passport;
                txtAddr.Text = currentStudent.Person.RegistrationAddress;
                if (currentStudent.Person.Father != null)
                {
                    txtFLN.Text = currentStudent.Person.Father.LastName;
                    txtFFN.Text = currentStudent.Person.Father.FirstName;
                    txtFMN.Text = currentStudent.Person.Father.MiddleName;
                }
                else {
                    txtFLN.Text = "";
                    txtFFN.Text = "";
                    txtFMN.Text = "";
                }
                if (currentStudent.Person.Mother != null)
                {
                    txtMLN.Text = currentStudent.Person.Mother.LastName;
                    txtMFN.Text = currentStudent.Person.Mother.FirstName;
                    txtMMN.Text = currentStudent.Person.Mother.MiddleName;
                }
                else
                {
                    txtMLN.Text = "";
                    txtMFN.Text = "";
                    txtMMN.Text = "";
                }
                cmbFaculty.SelectedIndex = faculties.FindIndex(f => f.Id == currentStudent.Group.Faculty.Id);               
                txtYear.Text = currentStudent.Group.StudyYear.ToString();
                txtGrpNumber.Text = currentStudent.Group.Number.ToString();

                List<OccupationRecord> occupations = (from o in context.OccupationSet
                            where o.Student.Person.Id == currentStudent.Person.Id
                            select new OccupationRecord
                            {
                                Id = o.Id,
                                Hostel = o.Room.Hostel.Name + " (" + o.Room.Hostel.Address + ")",
                                RoomNumber = o.Room.Number,
                                FromDate = o.FromDate,
                                ToDate = o.ToDate,
                                Order = (from oo in context.OrderSet
                                             where oo.Ocupation == o
                                             select new OccupationOrderRecord
                                             {
                                                 Price = oo.Price,
                                                 Number = oo.Number,
                                                 PaymentAmount = (from p in context.PaymentSet
                                                                  where p.Order == oo
                                                                  select p.Amount).DefaultIfEmpty().Sum()
                                             }
                                        ).FirstOrDefault()
                            }).ToList();
                occupations.ForEach(o => { o.From = o.FromDate.ToString("dd.MM.yyyy"); o.To = o.ToDate?.ToString("dd.MM.yyyy"); });
                occupations.Sort((r1, r2) => r2.FromDate.CompareTo(r1.FromDate));
                grdOHist.ItemsSource = occupations;

                List<PaymentRecord> payments = (from p in context.PaymentSet
                        where p.Order.Ocupation.Student.Person.Id == currentStudent.Person.Id
                        select new PaymentRecord
                        {
                            Id = p.Id,
                            Amount = p.Amount,
                            Number = p.Number,
                            PaymentDate = p.PaymentDate,
                            Order = new PaymentOrderRecord {
                                Price = p.Order.Price,
                                Number = p.Order.Number,
                                OrderDate = p.Order.OrderDate
                            }
                        }).ToList();
                payments.ForEach(p => {p.Date = p.PaymentDate.ToString("dd.MM.yyyy"); p.Order.Date = p.Order.OrderDate.ToString("dd.MM.yyyy"); });
                payments.Sort((r1, r2) => r2.PaymentDate.CompareTo(r1.PaymentDate));
                grdPHist.ItemsSource = payments;
           }
        }
    }
}
