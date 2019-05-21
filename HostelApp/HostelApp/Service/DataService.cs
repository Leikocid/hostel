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

        // попытка авторизации пользователя, если успекно то возврашается заполненный объект User
        public static User Login(String login, String password) {
            using (var context = new HostelModelContainer()) {
                User user = context.UserSet.Where(u => u.Login == login && u.Pasword == password && u.Active == true).FirstOrDefault();
                if (user != null) {
                    Person person = user.Person; // загрузить дополнительные данные в объект заранее
                }
                return user;
            }
        }

        // возвращает спсиок всех обежитий
        public static List<Hostel> GetAllHostels() {
            using (var context = new HostelModelContainer()) {
                return context.HostelSet.ToList();
            }
        }

        // поиск комнаты по общежитию и номеру
        public static Room FindRoom(int hostelId, int roomNumber) {
            using (var context = new HostelModelContainer()) {
                Room room = context.RoomSet.Where(r => r.Number == roomNumber && r.Hostel.Id == hostelId).FirstOrDefault();
                return room;
            }
        }

        // загрузка объекта Room и данных по Hostel
        public static Room LoadRoom(int roomId) {
            using (var context = new HostelModelContainer()) {
                Room room = context.RoomSet.Single(r => r.Id == roomId);
                Hostel hostel = room.Hostel; // предзагрузка данных про общежитиеы
                return room;
            }
        }

        // Поиск студентов проживающих в комнате
        public static List<StudentRecord> FindStudents(int roomId) {
            using (var context = new HostelModelContainer()) {
                return (from o in context.OccupationSet
                        where o.Room.Id == roomId && o.Student.Active == true
                        select o.Student).ToList().Select(s =>
                        new StudentRecord {
                            Id = s.Id,
                            LastName = s.Person.LastName,
                            FirstName = s.Person.FirstName,
                            MiddleName = s.Person.MiddleName,
                            FacultyName = s.Group.Faculty.Name,
                            StudyYear = s.Group.StudyYear,
                            GroupNumber = s.Group.Number
                        }).ToList();
            }
        }

        // Поиск комнат
        public class RoomRecord {
            public int Id { set; get; }
            public String Hostel { set; get; }
            public int Floor { set; get; }
            public int Number { set; get; }
            public int Capacity { set; get; }
            public int Occupied { set; get; }
            public int Free { set; get; }
        }

        public static List<RoomRecord> GetRooms(int hostelId) {
            using (var context = new HostelModelContainer()) {
                if (hostelId == -1) {
                    return (from r in context.RoomSet
                            select new RoomRecord {
                                Id = r.Id,
                                Hostel = r.Hostel.Name + "(" + r.Hostel.Address + ")",
                                Floor = r.Floor,
                                Number = r.Number,
                                Capacity = r.Capacity,
                                Occupied = (from o in context.OccupationSet
                                            where o.Room == r &&
                                                o.Active &&
                                                o.Student.Active &&
                                                o.FromDate <= DateTime.Today && (o.ToDate >= DateTime.Today || o.ToDate == null)
                                            select o).Count()
                            }).ToList();
                } else {
                    return (from r in context.RoomSet
                            where r.Hostel.Id == hostelId
                            select new RoomRecord {
                                Id = r.Id,
                                Hostel = r.Hostel.Name + " (" + r.Hostel.Address + ")",
                                Floor = r.Floor,
                                Number = r.Number,
                                Capacity = r.Capacity,
                                Occupied = (from o in context.OccupationSet
                                            where o.Room == r &&
                                                      o.Active &&
                                                      o.Student.Active &&
                                                      o.FromDate <= DateTime.Today && (o.ToDate >= DateTime.Today || o.ToDate == null)
                                            select o).Count()
                            }).ToList();
                }
            }
        }
    }
}
