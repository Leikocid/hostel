using HostelApp.Model;
using System;
using System.Linq;
using System.Windows;

namespace HostelApp.View {
    /// <summary>
    /// Interaction logic for EditOccupationWindow.xaml
    /// </summary>
    public partial class EditOccupationWindow : Window {
        public EditOccupationWindow() {
            InitializeComponent();
        }

        private int occupationId;
        private Occupation occupation;
        double payments = 0;
        public int OccupationId {
            get {
                return occupationId;
            }
            set {
                occupationId = value;
                UpdateForm();
            }
        }

        public void UpdateForm() {
            using (var context = new HostelModelContainer()) {
                tbxRoom.Text = occupation.Room.Hostel.Name + " (" + occupation.Room.Hostel.Address + ") комната " + occupation.Room.Number;
                tbxStudent.Text = occupation.Student.Person.LastName + " " + occupation.Student.Person.FirstName + " " + occupation.Student.Person.MiddleName + " - " +
                    occupation.Student.Group.Faculty.Name + " " + occupation.Student.Group.StudyYear + " курс " + occupation.Student.Group.Number + " группа";
                dpkFrom.SelectedDate = occupation.FromDate;
                if (occupation.ToDate != null) {
                    dpkTo.SelectedDate = occupation.ToDate;
                }
                if (occupation.Order != null) {
                    tbxPrice.Text = occupation.Order.Price.ToString();
                    if (occupation.Order.OrderDate != null) {
                        dpkOrder.SelectedDate = occupation.Order.OrderDate;
                    }
                    tbxOrderNumber.Text = occupation.Order.Number;
                    payments = (from p in context.PaymentSet
                                where p.Order.Id == occupation.Order.Id
                                select p.Amount).DefaultIfEmpty().Sum();
                }
                lblPayment.Content = payments.ToString();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e) {
            // проверки корректности
            String errorText = null;
            if (dpkFrom.SelectedDate == null) {
                errorText = "Дата заселения не указана";
            }
            if (dpkTo.SelectedDate != null && dpkFrom.SelectedDate != null && dpkFrom.SelectedDate > dpkTo.SelectedDate) {
                errorText = "Дата заселения должна быть больше даты выселения";
            }
            double price = 0;
            if (tbxPrice.Text.Length > 0 && !Double.TryParse(tbxPrice.Text, out price)) {
                errorText = "Ввеедено некорректное значение для цены";
            }
            if (price < 0) {
                errorText = "Цена не может быть отрицательной";
            }
            if (price > 0 && tbxOrderNumber.Text.Length == 0) {
                errorText = "Не задан номер счета";
            }
            if (price > 0 && dpkOrder.SelectedDate == null) {
                errorText = "Не задана дата платежа";
            }
            if (price == 0 && payments > 0) {
                errorText = "Нельзя удалить счет по которому есть платежи";
            }

            // изменения
            if (errorText == null) {
                using (var context = new HostelModelContainer()) {
                    context.Set(typeof(Occupation)).Attach(occupation);
                    occupation.FromDate = dpkFrom.SelectedDate.Value;
                    occupation.ToDate = dpkTo.SelectedDate;

                    Order order = occupation.Order;
                    if (price > 0) {
                        if (order == null) {
                            order = new Order() {
                                Ocupation = occupation
                            };
                            context.OrderSet.Add(order);
                        }
                        order.Price = price;
                        order.OrderDate = dpkOrder.SelectedDate.Value;
                        order.Number = tbxOrderNumber.Text;
                    }

                    if (order != null && price == 0) {
                        context.OrderSet.Remove(order);
                    }
                    context.SaveChanges();
                }
                Close();
            } else {
                lblError.Content = errorText;
            }
        }
    }
}
