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
using System.Windows.Shapes;

namespace HostelApp.View
{
    /// <summary>
    /// Interaction logic for AddPaymentWindow.xaml
    /// </summary>
    public partial class AddPaymentWindow : Window
    {
        public AddPaymentWindow()
        {
            InitializeComponent();
        }

        private int occupationId;
        private Occupation occupation;
        double payments = 0;
        public int OccupationId
        {
            get
            {
                return occupationId;
            }
            set
            {
                occupationId = value;
                UpdateForm();
            }
        }

        public void UpdateForm()
        {
            using (var context = new HostelModelContainer())
            {
                occupation = context.OccupationSet.Single(o => o.Id == occupationId);
                tbxRoom.Text = occupation.Room.Hostel.Name + " (" + occupation.Room.Hostel.Address + ") комната " + occupation.Room.Number;
                tbxStudent.Text = occupation.Student.Person.LastName + " " + occupation.Student.Person.FirstName + " " + occupation.Student.Person.MiddleName + " - " +
                    occupation.Student.Group.Faculty.Name + " " + occupation.Student.Group.StudyYear + " курс " + occupation.Student.Group.Number + " группа";
                dpkFrom.Text = occupation.FromDate.ToString("dd.MM.yyyy");
                if (occupation.ToDate != null)
                {
                    dpkTo.Text = occupation.ToDate?.ToString("dd.MM.yyyy");
                }
                if (occupation.Order != null)
                {
                    tbxPrice.Text = occupation.Order.Price.ToString();
                    if (occupation.Order.OrderDate != null)
                    {
                        dpkOrder.Text = occupation.Order.OrderDate.ToString("dd.MM.yyyy");
                    }
                    tbxOrderNumber.Text = occupation.Order.Number;
                    payments = (from p in context.PaymentSet
                                where p.Order.Id == occupation.Order.Id
                                select p.Amount).DefaultIfEmpty().Sum();
                }
                lblPayment.Content = payments.ToString();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            // проверки корректности
            String errorText = null;
            if (dpkPaymentDate.SelectedDate == null)
            {
                errorText = "Дата оплаты не указана";
            }
            double amount = 0;
            if (tbxAmount.Text.Length > 0 && !Double.TryParse(tbxAmount.Text, out amount))
            {
                errorText = "Ввеедено некорректное значение для стоимости оплаты";
            }
            if (amount < 0)
            {
                errorText = "Стоимость оплаты не может быть отрицательной";
            }
            if (amount > 0 && tbxPaymentNumber.Text.Length == 0)
            {
                errorText = "Не задан номер счета";
            }

            // изменения
            if (errorText == null)
            {
                using (var context = new HostelModelContainer())
                {
                    context.Set(typeof(Occupation)).Attach(occupation);
                    Order order = occupation.Order;
                    Payment payment = new Payment() {
                        Order = order,
                        Amount = amount,
                        PaymentDate = dpkPaymentDate.SelectedDate.Value,
                        Number = tbxPaymentNumber.Text
                    };
                    context.PaymentSet.Add(payment);
                    context.SaveChanges();
                }
                Close();
            }
            else
            {
                lblError.Content = errorText;
            }
        }
    }
}
