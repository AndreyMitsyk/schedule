namespace Scheduler.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Models;

    /// <summary>
    /// Тело Home Controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Имя cookie авторизации.
        /// </summary>
        private const string CookieName = "scheduler_isuct";
        /// <summary>
        /// Значение cookie для админа. TODO: будет заменено генерацией MD5.
        /// </summary>
        private const string Adminmd = "21232f297a57a5a743894a0e4a801fc3";

        /// <summary>
        /// БД.
        /// </summary>
        readonly Db _db = new Db();

        /// <summary>
        /// Объявление бд.
        /// </summary>
        public HomeController()
        {
            ViewBag.Db = _db;
        }

        /// <summary>
        /// Главная страница.
        /// </summary>
        /// <param name="facultyId">ID факультета.</param>
        /// <param name="courseId">ID курса</param>
        /// <param name="groupId">ID группы</param>
        /// <param name="weekNumberId">ID номера недели</param>
        /// <returns>Расписание для группы.</returns>
        public ActionResult Index(int facultyId = 1, int courseId = 1, int groupId = 1, byte weekNumberId = 1)
        {
            // Проверка cookies.
            CheckCookies();

            // Загрузка данных расписания из БД.
            var items = _db.ScheduleItems.Include("LessonTime").Include("Subject")
                 .Include("LessonType").Include("Teacher").Include("Auditorium")
                 .Where(model => model.Group.Faculty.Id == facultyId && (DateTime.Now.Year - model.Group.YearOfReceipt) == courseId &&
                                 model.GroupId == groupId && model.WeekNumber == weekNumberId).ToArray();

            var search = new SearchModel
            {
                FacultyId = facultyId,
                CourseId = courseId,
                GroupId = groupId,
                WeekNumberId = weekNumberId,
            };
            return View(new AdminModel { Items = items, Search = search });
        }

        /// <summary>
        /// Загрузка расписания.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Расписание для группы.</returns>
        [HttpPost]
        public ActionResult FindForUser(AdminModel model)
        {
            // TODO: запоминать выбранное в последний раз расписание
            return RedirectToAction("Index", new
            {
                model.Search.FacultyId,
                model.Search.CourseId,
                model.Search.GroupId,
                model.Search.WeekNumberId,
            });
        }

        /// <summary>
        /// Страница управления расписанием.
        /// </summary>
        /// <param name="facultyId">ID факультета</param>
        /// <param name="courseId">ID курса</param>
        /// <param name="groupId">ID группы</param>
        /// <param name="weekNumberId">ID номера недели</param>
        /// <param name="dayOfWeekItemId">ID дня недели</param>
        /// <returns></returns>
        public ActionResult Admin(int facultyId = 1, int courseId = 1, int groupId = 1, byte weekNumberId = 1,
            int dayOfWeekItemId = 1)
        {
            CheckCookies();

            string cookieValue = null;

            if (Request.Cookies[CookieName] != null)
                cookieValue = Request.Cookies[CookieName].Value;

            if (cookieValue!=Adminmd | cookieValue == null)
                return RedirectToAction("Index");

            var items = _db.ScheduleItems.Include("LessonTime").Include("Subject")
                .Include("LessonType").Include("Teacher").Include("Auditorium")
                .Where(model => model.Group.Faculty.Id == facultyId && 
                                model.GroupId == groupId && model.WeekNumber == weekNumberId &&
                                model.DayOfWeekItemId == dayOfWeekItemId).ToList();

            if (!items.Any())
                items.AddRange(_db.LessonTimes.ToArray().Select(lessonTime => _db.ScheduleItems.Add(new ScheduleItem
                {
                    GroupId = groupId,
                    WeekNumber = weekNumberId,
                    DayOfWeekItemId = dayOfWeekItemId,
                    LessonTimeId = lessonTime.Id,
                })));
            _db.SaveChanges();

            var search = new SearchModel
            {
                FacultyId = facultyId,
                CourseId = courseId,
                GroupId = groupId,
                WeekNumberId = weekNumberId,
                DayOfWeekItemId = dayOfWeekItemId,
            };
            return View(new AdminModel { Items = items.ToArray(), Search = search });
        }

        /// <summary>
        /// Сохранение данных расписания.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(AdminModel model)
        {
            foreach (var item in model.Items)
            {
                item.Group.Faculty.Id = model.Search.FacultyId;
                item.GroupId = model.Search.GroupId;
                item.WeekNumber = model.Search.WeekNumberId;
                item.DayOfWeekItemId = model.Search.DayOfWeekItemId;

                _db.Entry(item).State = EntityState.Modified;
            }

            _db.SaveChanges();
            return RedirectToAction("Admin", new
            {
                model.Search.FacultyId,
                model.Search.CourseId,
                model.Search.GroupId,
                model.Search.WeekNumberId,
                model.Search.DayOfWeekItemId,
            });
        }

        /// <summary>
        /// Загрузка расписания на странице админа.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Расписание для группы</returns>
        [HttpPost]
        public ActionResult FindForAdmin(AdminModel model)
        {
            return RedirectToAction("Admin", new
            {
                model.Search.FacultyId,
                model.Search.CourseId,
                model.Search.GroupId,
                model.Search.WeekNumberId,
                model.Search.DayOfWeekItemId
            });
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Регистрация.
        /// </summary>
        /// <returns>Форма регистрации.</returns>
        public ActionResult SignUp()
        {
            if (Request.Cookies[CookieName] != null)
                return RedirectToAction("Index");

            return View();
        }

        /// <summary>
        /// Регистрация.
        /// </summary>
        /// <param name="user">Данные пользователя.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            using (var db = new Db())
            {
                var firstOrDefault = db.Users.FirstOrDefault(user1 => user1.Email == user.Email);
                if (firstOrDefault != null)
                {
                    return View();
                }
                db.Users.Add(user);
                db.SaveChanges();
            }
            return RedirectToAction("SignIn");
        }

        /// <summary>
        /// Логин.
        /// </summary>
        /// <returns>Форма логина.</returns>
        public ActionResult SignIn()
        {
            if (Request.Cookies[CookieName] != null)
                return RedirectToAction("Index");

            return View();
        }

        /// <summary>
        /// Логин.
        /// </summary>
        /// <param name="user">Данные для авторизации.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignIn(User user)
        {
            using (var db = new Db())
            {
               var u = db.Users.Include("Roles").FirstOrDefault(user1 => user1.Email == user.Email & user1.Password == user.Password);
                if (u != null)
                {
                    ViewBag.Error = false;
                    HttpCookie httpCookie = Response.Cookies[CookieName];
                    Role role = db.Roles.FirstOrDefault(role1 => role1.RoleName == "admin");
                    if (true) //u.Roles.Contains(role))
                    {
                        // TODO: md5 generatoin.
                        if (httpCookie != null)
                        {
                            httpCookie.Value = Adminmd;
                            httpCookie.Expires = DateTime.Now.AddDays(7);
                        }
                        return RedirectToAction("Admin");
                    }

                    if (httpCookie != null)
                    {
                        httpCookie.Value = "User_login";
                        httpCookie.Expires = DateTime.Now.AddDays(7);
                    }
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Error = true;
            return View(user);
        }

        /// <summary>
        /// Выход из системы.
        /// </summary>
        /// <returns>Главная страница.</returns>
        public ActionResult SignOut()
        {
            HttpCookie httpCookie = Response.Cookies[CookieName];
            if (httpCookie != null)
                httpCookie.Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Проверка Cookies и активация/деактивация элементов верхней панели.
        /// </summary>
        private void CheckCookies()
        {
            if (Request.Cookies[CookieName] != null)
            {
                ViewBag.ShowSignIn = false;
                ViewBag.ShowSignOut = true;

                if (Request.Cookies[CookieName].Value == Adminmd)
                {
                    ViewBag.ShowAdmin = true;
                }
            }
            else
            {
                ViewBag.ShowSignIn = true;
                ViewBag.ShowAdmin = false;
                ViewBag.ShowSignOut = false;
            }
        }
    }
}