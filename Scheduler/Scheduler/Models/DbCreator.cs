namespace Scheduler.Models
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Создание БД.
    /// </summary>
    public static class DbCreator
    {
        /// <summary>
        /// Файлов с данными.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Данные файла.</returns>
        private static IEnumerable<string> GetFileData(string name)
        {
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "DbStaticData", name + ".txt");
            return !File.Exists(path) ? new string[0] : File.ReadAllLines(path);
        }

        /// <summary>
        /// Действия пр запуске.
        /// </summary>
        public static void Run()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Db>());

            // Внесение данных в БД.
            using (var db = new Db())
            {
                if (!db.Auditoriums.Any())
                {
                    foreach (var s in GetFileData("Auditoriums"))
                    {
                        db.Auditoriums.Add(new Auditorium {Name = s});
                    }
                }
                if (!db.Courses.Any())
                {
                    foreach (var s in GetFileData("Courses"))
                    {
                        db.Courses.Add(new Course {Name = s});
                    }
                }
                if (!db.DayOfWeekItems.Any())
                {
                    foreach (var s in GetFileData("DayOfWeekItems"))
                    {
                        db.DayOfWeekItems.Add(new DayOfWeekItem {Name = s});
                    }
                }
                if (!db.Faculties.Any())
                {
                    foreach (var s in GetFileData("Faculties"))
                    {
                        db.Faculties.Add(new Faculty {Name = s});
                    }
                }
                if (!db.Groups.Any())
                {
                    foreach (var s in GetFileData("Groups"))
                    {
                        db.Groups.Add(new Group {Name = s});
                    }
                }

                if (!db.LessonTypes.Any())
                {
                    foreach (var s in GetFileData("LessonTypes"))
                    {
                        db.LessonTypes.Add(new LessonType {Name = s});
                    }
                }
                if (!db.Subjects.Any())
                {
                    foreach (var s in GetFileData("Subjects"))
                    {
                        db.Subjects.Add(new Subject {Name = s});
                    }
                }
                if (!db.Teachers.Any())
                {
                    foreach (var s in GetFileData("Teachers"))
                    {
                        db.Teachers.Add(new Teacher {Name = s});
                    }
                }
                if (!db.WeekNumbers.Any())
                {
                    foreach (var s in GetFileData("WeekNumbers"))
                    {
                        db.WeekNumbers.Add(new WeekNumber {Name = s});
                    }
                }

                if (!db.LessonTimes.Any())
                {
                    foreach (var s in GetFileData("LessonTimes"))
                    {
                        db.LessonTimes.Add(new LessonTime {Name = s});
                    }
                }

                if (!db.Roles.Any())
                {
                    foreach (var s in GetFileData("Roles"))
                    {
                        db.Roles.Add(new Role {RoleName = s});
                    }
                }

                db.SaveChanges();

                //-------------------------------------------------------------
                // TODO: убрать позже.
                if (!db.Users.Any())
                {
                    User admin = new User
                    {
                        Email = "mitzyk@mail.ru",
                        FirstName = "Andrey",
                        LastName = "Mitsyk",
                        Password = "12345",
                        Role = db.Roles.Find(1)
                    };

                    db.Users.Add(admin);
                }

                db.SaveChanges();
                //-------------------------------------------------------------
            }
        }
    }
}