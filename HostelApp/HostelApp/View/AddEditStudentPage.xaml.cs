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
            public bool Active { set; get; }
            public String ActiveText { set; get; }
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
            lblOccuoationStatus.Content = "не заселен";
            grdOHist.ItemsSource = new List<OccupationRecord>();
            lblPaymentStatus.Content = "долга нет";
            grdPHist.ItemsSource = new List<PaymentRecord>();
            lblError.Content = "";
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
                Student student = context.StudentSet.Single(s => s.Id == studentId);
                lblMode.Content = student.Person.LastName + " " + student.Person.FirstName + " " + student.Person.MiddleName;
                txtLN.Text = student.Person.LastName;
                txtFN.Text = student.Person.FirstName;
                txtMN.Text = student.Person.MiddleName;
                txtPasp.Text = student.Person.Passport;
                txtAddr.Text = student.Person.RegistrationAddress;
                if (student.Person.Father != null)
                {
                    txtFLN.Text = student.Person.Father.LastName;
                    txtFFN.Text = student.Person.Father.FirstName;
                    txtFMN.Text = student.Person.Father.MiddleName;
                }
                else {
                    txtFLN.Text = "";
                    txtFFN.Text = "";
                    txtFMN.Text = "";
                }
                if (student.Person.Mother != null)
                {
                    txtMLN.Text = student.Person.Mother.LastName;
                    txtMFN.Text = student.Person.Mother.FirstName;
                    txtMMN.Text = student.Person.Mother.MiddleName;
                }
                else
                {
                    txtMLN.Text = "";
                    txtMFN.Text = "";
                    txtMMN.Text = "";
                }
                cmbFaculty.SelectedIndex = faculties.FindIndex(f => f.Id == student.Group.Faculty.Id);               
                txtYear.Text = student.Group.StudyYear.ToString();
                txtGrpNumber.Text = student.Group.Number.ToString();

                List<OccupationRecord> occupations = (from o in context.OccupationSet
                            where o.Student.Person.Id == student.Person.Id
                            select new OccupationRecord
                            {
                                Id = o.Id,
                                Active = o.Active,
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
                OccupationRecord currentOccupation = null;
                double dept = 0;
                occupations.ForEach(o => {
                    o.From = o.FromDate.ToString("dd.MM.yyyy");
                    o.To = o.ToDate?.ToString("dd.MM.yyyy");
                    o.ActiveText = o.Active ? "актуально" : "архив";
                    if (o.Active && o.FromDate <= DateTime.Today && (o.ToDate >= DateTime.Today || o.ToDate == null)) {
                        currentOccupation = o;
                    }
                    if (o.Order != null)
                    {
                        dept += o.Order.Price;
                    }
                });
                occupations.Sort((r1, r2) => r2.FromDate.CompareTo(r1.FromDate));
                if (currentOccupation == null)
                {
                    lblOccuoationStatus.Content = "не заселен";
                }
                else {
                    lblOccuoationStatus.Content = "заселен - " + currentOccupation.Hostel + ", комната " + currentOccupation.RoomNumber;
                }
               
                grdOHist.ItemsSource = new List<OccupationRecord>();
                grdOHist.ItemsSource = occupations;

                List<PaymentRecord> payments = (from p in context.PaymentSet
                        where p.Order.Ocupation.Student.Person.Id == student.Person.Id
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
                payments.ForEach(p => {
                    p.Date = p.PaymentDate.ToString("dd.MM.yyyy");
                    p.Order.Date = p.Order.OrderDate.ToString("dd.MM.yyyy");
                    dept -= p.Amount;
                });
                payments.Sort((r1, r2) => r2.PaymentDate.CompareTo(r1.PaymentDate));

                if (dept > 0.001)
                {
                    lblPaymentStatus.Content = "долг студента = " + dept.ToString();
                }
                else if (dept < -0.001)
                {
                    lblPaymentStatus.Content = "переплата студента = " + (-dept).ToString();
                }
                else {
                    lblPaymentStatus.Content = "долга нет";
                }
                grdPHist.ItemsSource = payments;
                lblError.Content = "";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // проверка корректности полей
            String error = null;
            lblError.Content = "";

            if (txtLN.Text.Length == 0) {
                error = "Фамилия студента не может быть пустой";
            }
            if (txtFN.Text.Length == 0)
            {
                error = "Имя студента не может быть пустым";
            }
            if (cmbFaculty.SelectedIndex == -1)
            {
                error = "Не задан факультет";
            }
            int year = 0;
            if (txtYear.Text.Length == 0 || !Int32.TryParse(txtYear.Text, out year))
            {
               error = "Год обучения задан некорректно";
            }
            if (year < 1 || year > 4) {
                error = "Год обучения должен быть от 1 до 4";
            }
            int number = 0;
            if (txtGrpNumber.Text.Length == 0 || !Int32.TryParse(txtGrpNumber.Text, out number))
            {
                error = "Номер группы задан некорректно";
            }
            int facultyId = faculties[cmbFaculty.SelectedIndex].Id;
            using (var context = new HostelModelContainer())
            {
               
                Group group = (from g in context.GroupSet
                               where g.Faculty.Id == facultyId && g.StudyYear == year && g.Number == number
                               select g).FirstOrDefault();
                if (group == null) {
                    error = "Указанная группа не найдена";
                }
            }
            if (error == null) { 
                if (currentStudentId == -1) {
                    int studentId = CreateNewStudent();
                    SwitchToEditMode(studentId);
                } else {
                    using (var context = new HostelModelContainer())
                    {
                        Student student = context.StudentSet.Single(s => s.Id == currentStudentId);
                        if (student.Group.Number != number || student.Group.StudyYear != year || student.Group.Faculty.Id != facultyId)
                        {
                            DeactivateStudent(student.Id);
                            int studentId = CreateNewStudent();
                            SwitchToEditMode(studentId);
                        }
                        else {
                            UpdateStudent(student.Id);
                            SwitchToEditMode(student.Id);
                        }
                    }
                }
            }
            else {
                lblError.Content = error;
            }
        }

        private void UpdateStudent(int studentId)
        {
            using (var context = new HostelModelContainer())
            {
                // обновление информации про студента
                Student student = context.StudentSet.Single(s => s.Id == studentId);
                Person person = student.Person;
                person.FirstName = txtFN.Text;
                person.LastName = txtLN.Text;
                person.MiddleName = txtMN.Text;
                person.Passport = txtPasp.Text;
                person.RegistrationAddress = txtAddr.Text;
                Person father = person.Father;
                if (father == null)
                {
                    father = new Person();
                    context.PersonSet.Add(father);
                    person.Father = father;
                }
                father.FirstName = txtFFN.Text;
                father.LastName = txtFLN.Text;
                father.MiddleName = txtFMN.Text;
                Person mother = person.Mother;
                if (mother == null)
                {
                    mother = new Person();
                    context.PersonSet.Add(mother);
                    person.Mother = mother;
                }
                mother.FirstName = txtMFN.Text;
                mother.LastName = txtMLN.Text;
                mother.MiddleName = txtMMN.Text;
                context.SaveChanges();
            }
        }

        private void DeactivateStudent(int studentId)
        {
            //  деактивация студента
            using (var context = new HostelModelContainer())
            {
                Student student = context.StudentSet.Single(s => s.Id == studentId);
                student.Active = false;
                context.SaveChanges();
            }
        }

        private int CreateNewStudent()
        {
            using (var context = new HostelModelContainer())
            {
                // создание нового студента              
                Person person = new Person();
                context.PersonSet.Add(person);
                person.FirstName = txtFN.Text;
                person.LastName = txtLN.Text;
                person.MiddleName = txtMN.Text;
                person.Passport = txtPasp.Text;
                person.RegistrationAddress = txtAddr.Text;
                Person father = new Person();
                context.PersonSet.Add(father);
                person.Father = father;
                father.FirstName = txtFFN.Text;
                father.LastName = txtFLN.Text;
                father.MiddleName = txtFMN.Text;
                Person mother = new Person();
                context.PersonSet.Add(mother);
                person.Mother = mother;
                mother.FirstName = txtMFN.Text;
                mother.LastName = txtMLN.Text;
                mother.MiddleName = txtMMN.Text;

                Student student = new Student();
                context.StudentSet.Add(student);
                student.Person = person;
                int facultyId = faculties[cmbFaculty.SelectedIndex].Id;
                int year = Int32.Parse(txtYear.Text);
                int number = Int32.Parse(txtGrpNumber.Text);
                Group group = (from g in context.GroupSet
                               where g.Faculty.Id == facultyId && g.StudyYear == year && g.Number == number
                               select g).FirstOrDefault();
                student.Group = group;
                student.Active = true;
                context.SaveChanges();
                return student.Id;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (currentStudentId != -1) {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Вы уверены что хотите удалить студента?", "Подтверждение действия", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DeactivateStudent(currentStudentId);
                    SwitchToNewMode();
                }
            }
        }
    }
}
