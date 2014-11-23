using System;
using System.Linq;
using System.Web.Mvc;
using Antlr.Runtime;
using Scheduler.Models;

namespace Scheduler.Controllers
{
    public class HomeController : Controller
    {
        public const string CookieName = "scheduler_isuct";

        public ActionResult Index()
        {
            return View();
        }

        public Models.Scheduler[] GetSchedules()
        {
            int faculty;
            int.TryParse(Request["faculty"] ?? "1", out faculty) ;
            int course;
            int.TryParse(Request["course"] ?? "1", out course) ;
            int group;
            int.TryParse(Request["group"] ?? "1", out group) ;
            int week;
            int.TryParse(Request["week"] ?? "1", out week) ;

            using (var db = new Db())
            {
                db.Scheduler.Where(s => s.Faculty.Id == faculty);
            }
            return null;
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

    }
}