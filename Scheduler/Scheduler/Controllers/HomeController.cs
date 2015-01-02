namespace Scheduler.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Models;

    /// <summary>
    /// ���� Home Controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// ��� cookie �����������.
        /// </summary>
        public const string CookieName = "scheduler_isuct";
        /// <summary>
        /// �������� cookie ��� ������. TODO: ����� �������� ���������� MD5.
        /// </summary>
        private const string Adminmd = "21232f297a57a5a743894a0e4a801fc3";

        /// <summary>
        /// ��.
        /// </summary>
        readonly Db _db = new Db();

        /// <summary>
        /// ���������� ��.
        /// </summary>
        public HomeController()
        {
            ViewBag.Db = _db;
        }

        /// <summary>
        /// ������� ��������.
        /// </summary>
        /// <param name="facultyId">ID ����������.</param>
        /// <param name="courseId">ID �����</param>
        /// <param name="groupId">ID ������</param>
        /// <param name="weekNumberId">ID ������ ������</param>
        /// <returns>���������� ��� ������.</returns>
        public ActionResult Index(int facultyId = 1, int courseId = 1, int groupId = 1, int weekNumberId = 1)
        {
            // �������� cookies.
            CheckCookies();

            // �������� ������ ���������� �� ��.
            var items = _db.ScheduleItems.Include("LessonTime").Include("Subject")
                 .Include("LessonType").Include("Teacher").Include("Auditorium")
                 .Where(model => model.FacultyId == facultyId && model.CourseId == courseId &&
                                 model.GroupId == groupId && model.WeekNumberId == weekNumberId).ToArray();

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
        /// �������� ����������.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>���������� ��� ������.</returns>
        [HttpPost]
        public ActionResult FindForUser(AdminModel model)
        {
            // TODO: ���������� ��������� � ��������� ��� ����������
            return RedirectToAction("Index", new
            {
                model.Search.FacultyId,
                model.Search.CourseId,
                model.Search.GroupId,
                model.Search.WeekNumberId,
            });
        }

        /// <summary>
        /// �������� ���������� �����������.
        /// </summary>
        /// <param name="facultyId">ID ����������</param>
        /// <param name="courseId">ID �����</param>
        /// <param name="groupId">ID ������</param>
        /// <param name="weekNumberId">ID ������ ������</param>
        /// <param name="dayOfWeekItemId">ID ��� ������</param>
        /// <returns></returns>
        public ActionResult Admin(int facultyId = 1, int courseId = 1, int groupId = 1, int weekNumberId = 1,
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
                .Where(model => model.FacultyId == facultyId && model.CourseId == courseId &&
                                model.GroupId == groupId && model.WeekNumberId == weekNumberId &&
                                model.DayOfWeekItemId == dayOfWeekItemId).ToList();

            if (!items.Any())
                items.AddRange(_db.LessonTimes.ToArray().Select(lessonTime => _db.ScheduleItems.Add(new ScheduleItem
                {
                    FacultyId = facultyId,
                    CourseId = courseId,
                    GroupId = groupId,
                    WeekNumberId = weekNumberId,
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
                DayOfWeekItemId = dayOfWeekItemId
            };
            return View(new AdminModel { Items = items.ToArray(), Search = search });
        }

        /// <summary>
        /// ���������� ������ ����������.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(AdminModel model)
        {
            foreach (var item in model.Items)
            {
                item.FacultyId = model.Search.FacultyId;
                item.CourseId = model.Search.CourseId;
                item.GroupId = model.Search.GroupId;
                item.WeekNumberId = model.Search.WeekNumberId;
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
        /// �������� ���������� �� �������� ������.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>���������� ��� ������</returns>
        [HttpPost]
        public ActionResult FindForAdmin(AdminModel model)
        {
            return RedirectToAction("Admin", new
            {
                model.Search.FacultyId,
                model.Search.CourseId,
                model.Search.GroupId,
                model.Search.WeekNumberId,
                model.Search.DayOfWeekItemId,
            });
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// �����������.
        /// </summary>
        /// <returns>����� �����������.</returns>
        public ActionResult SignUp()
        {
            if (Request.Cookies[CookieName] != null)
                return RedirectToAction("Index");

            return View();
        }

        /// <summary>
        /// �����������.
        /// </summary>
        /// <param name="user">������ ������������.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            using (var db = new Db())
            {
                user.Role = db.Roles.FirstOrDefault(role1 => role1.RoleName == "user");
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
        /// �����.
        /// </summary>
        /// <returns>����� ������.</returns>
        public ActionResult SignIn()
        {
            if (Request.Cookies[CookieName] != null)
                return RedirectToAction("Index");

            return View();
        }

        /// <summary>
        /// �����.
        /// </summary>
        /// <param name="user">������ ��� �����������.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignIn(User user)
        {
            using (var db = new Db())
            {
               var u = db.Users.Include("Role").FirstOrDefault(user1 => user1.Email == user.Email & user1.Password == user.Password);
                if (u != null)
                {
                    ViewBag.Error = false;

                    if (u.Role.RoleName == "admin")
                    {
                        // TODO: md5 generatoin.
                        Response.Cookies[CookieName].Value = Adminmd;
                        Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(7);
                        return RedirectToAction("Admin");
                    }

                    Response.Cookies[CookieName].Value = "User_login";
                    Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(7);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Error = true;
            return View(user);
        }

        /// <summary>
        /// ����� �� �������.
        /// </summary>
        /// <returns>������� ��������.</returns>
        public ActionResult SignOut()
        {
            if (Request.Cookies[CookieName] != null)
            {
                Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// �������� Cookies � ���������/����������� ��������� ������� ������.
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