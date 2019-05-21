using HostelApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostelApp.Service {
    /// <summary>
    /// Interaction with Database
    /// </summary>
    class DataService {

        //  подсчитать количество всех комнат в системе
        public static int GetRoomsCount() {
            using (var context = new HostelModelContainer()) {
                return context.RoomSet.Sum(r => r.Capacity);
            }
        }

        // подсчитать количество занятых комнат 
        public static int GetOccupiedRoomsCount() {
            using (var context = new HostelModelContainer()) {
                return context.OccupationSet.Where(o =>
                    o.Active &&
                    o.Student.Active &&
                    o.FromDate <= DateTime.Today && (o.ToDate >= DateTime.Today || o.ToDate == null)
                ).Count();
            }
        }

        // продсчитать сумму всех выставленных счетов
        public static double GetOrdersSum() {
            using (var context = new HostelModelContainer()) {
                return context.OrderSet.Sum(o => (double?)o.Price) ?? 0;
            }
        }

        // продсчитать сумму всех оплат
        public static double GetPaymentsSum() {
            using (var context = new HostelModelContainer()) {
                return context.PaymentSet.Sum(p => (double?)p.Amount) ?? 0;
            }
        }

        // поиск студентов и извлечение информации для таблицы
        public class StudentOcupation {
            public String Hostel { set; get; }
            public int RoomNumber { set; get; }
        }

        public class StudentRecord {
            public int Id { set; get; }
            public String LastName { set; get; }
            public String FirstName { set; get; }
            public String MiddleName { set; get; }
            public String FacultyName { set; get; }
            public int StudyYear { set; get; }
            public int GroupNumber { set; get; }
            public StudentOcupation StudentOcupation { set; get; }
        }

        public static List<StudentRecord> FindStudents(String text) {
            using (var context = new HostelModelContainer()) {
                if (text != null && text.Length > 0) {
                    var query = context.StudentSet.Where(s => s.Active && (
                        s.Person.FirstName.ToLower().StartsWith(text.ToLower()) ||
                        s.Person.LastName.ToLower().StartsWith(text.ToLower()))).Take(10).Select(s =>
                    new StudentRecord {
                        Id = s.Id,
                        LastName = s.Person.LastName,
                        FirstName = s.Person.FirstName,
                        MiddleName = s.Person.MiddleName,
                        FacultyName = s.Group.Faculty.Name,
                        StudyYear = s.Group.StudyYear,
                        GroupNumber = s.Group.Number,
                        StudentOcupation = (
                                from o in context.OccupationSet
                                where o.Student == s &&
                                        o.Active && o.Student.Active &&
                                        o.FromDate <= DateTime.Today && (o.ToDate >= DateTime.Today || o.ToDate == null)
                                select
                                new StudentOcupation {
                                    Hostel = o.Room.Hostel.Name + "\n" + o.Room.Hostel.Address,
                                    RoomNumber = o.Room.Number
                                }).FirstOrDefault()
                    });
                    return query.ToList();
                } else {
                    var query = context.StudentSet.Where(s => s.Active).Take(10).Select(s =>
                        new StudentRecord {
                            Id = s.Id,
                            LastName = s.Person.LastName,
                            FirstName = s.Person.FirstName,
                            MiddleName = s.Person.MiddleName,
                            FacultyName = s.Group.Faculty.Name,
                            StudyYear = s.Group.StudyYear,
                            GroupNumber = s.Group.Number,
                            StudentOcupation = (
                                    from o in context.OccupationSet
                                    where o.Student == s &&
                                            o.Active && o.Student.Active &&
                                            o.FromDate <= DateTime.Today && (o.ToDate >= DateTime.Today || o.ToDate == null)
                                    select
                                    new StudentOcupation {
                                        Hostel = o.Room.Hostel.Name + "\n" + o.Room.Hostel.Address,
                                        RoomNumber = o.Room.Number
                                    }).FirstOrDefault()
                        });
                    return query.ToList();
                }
            }
        }
    }
}
