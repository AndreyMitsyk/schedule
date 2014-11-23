using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Antlr.Runtime;
using Microsoft.Data.Edm.Csdl;
using Scheduler.Models;

namespace Scheduler.Controllers
{
    public class HomeController : Controller
    {

        public const string CookieName = "scheduler_isuct";
        private const string Adminmd = "21232f297a57a5a743894a0e4a801fc3";

        readonly Db _db = new Db();

        public HomeController()
        {
            ViewBag.Db = _db;
        }
        public ActionResult Index(int facultyId = 1, int courseId = 1, int groupId = 1, int weekNumberId = 1)
        {
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



        [HttpPost]
        public ActionResult FindForUser(AdminModel model)
        {
            return RedirectToAction("Index", new
            {
                model.Search.FacultyId,
                model.Search.CourseId,
                model.Search.GroupId,
                model.Search.WeekNumberId,
            });
        }

        public ActionResult Admin(int facultyId = 1, int courseId = 1, int groupId = 1, int weekNumberId = 1,
            int dayOfWeekItemId = 1)
        {
            string CookieValue = null;

            if (Request.Cookies[CookieName] != null)
                CookieValue = Request.Cookies[CookieName].Value;

            if (CookieValue!=Adminmd | CookieValue == null)
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



        //        public Models.Scheduler[] GetSchedules()
        //        {

        //        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);

        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            using (var db = new Db())
            {
                user.Role = new Role(){Id=2, RoleName = "user"};
                db.Users.Add(user);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult SignIn()
        {
            if (Request.Cookies[CookieName] != null)
                return RedirectToAction("Index");

            return View();
        }

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

        public ActionResult SignOut()
        {
            if (Request.Cookies[CookieName] != null)
            {
                Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index");
        }

    }
}