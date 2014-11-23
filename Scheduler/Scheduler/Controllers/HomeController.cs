using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Antlr.Runtime;
using Scheduler.Models;

namespace Scheduler.Controllers
{
    public class HomeController : Controller
    {

        public const string CookieName = "scheduler_isuct";

                readonly Db _db = new Db();

        private int _number;
        private int _facultyId;
        private int _courseId;
        private int _groupId;
        private int _weekNumberId;
        private int _dayOfWeekItemId;

        public HomeController()
        {
            ViewBag.Db = _db;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            _db.Auditoriums.Add(new Auditorium {Name = "asfa"});
            _db.SaveChanges();

            int.TryParse(Request["number"] ?? "0", out _number);
            int.TryParse(Request["faculty"] ?? "0", out _facultyId);
            int.TryParse(Request["course"] ?? "0", out _courseId);
            int.TryParse(Request["group"] ?? "0", out _groupId);
            int.TryParse(Request["dayOfWeekItem"] ?? "0", out _dayOfWeekItemId);
            int.TryParse(Request["weekNumber"] ?? "0", out _weekNumberId);

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
           var adminModel = _db.AdminModels.Find(_number) ?? new AdminModel();

//            var model = new AdminModel
//            {
//                Id = _number,
//                FacultyId = _facultyId,
//                CourseId = _courseId,
//                GroupId = _groupId,
//                DayOfWeekItemId = _dayOfWeekItemId,
//                WeekNumberId = _weekNumberId,
//                ScheduleItems = 
//            };
            var schedules = (adminModel.ScheduleItems ?? new ScheduleItem[0]).ToList();
            schedules.Add(new ScheduleItem());
            adminModel.ScheduleItems = schedules.ToArray();
           return View(adminModel);
        }

        [HttpPost]
        public ActionResult Admin(AdminModel model)
        {
            _db.AdminModels.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Admin");
        }

        //        public Models.Scheduler[] GetSchedules()
        //        {
        //            int faculty;
        //            int.TryParse(Request["faculty"] ?? "1", out faculty) ;
        //            int course;
        //            int.TryParse(Request["course"] ?? "1", out course) ;
        //            int group;
        //            int.TryParse(Request["group"] ?? "1", out group) ;
        //            int week;
        //            int.TryParse(Request["week"] ?? "1", out week) ;
        //
        //            using (var db = new Db())
        //            {
        //                db.Scheduler.Where(s => s.Faculty.Id == faculty);
        //            }
        //            return null;
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
                user.Role = new Role(){Id=1, RoleName = "admin"};
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
               var u = db.Users.FirstOrDefault(user1 => user1.Email == user.Email & user1.Password == user.Password);
                if (u != null)
                {
                    ViewBag.Error = false;

                    Response.Cookies[CookieName].Value = "Login";
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