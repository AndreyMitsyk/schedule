using System.Web.Mvc;
using System.Web.Security;
using Scheduler.Models;

namespace Scheduler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new Db())
            {
                db.Roles.Add(new Role() {RoleName = "Admin"});
                db.Users.Add(new User()
                {
                    Email = "mitzyk@mail.ru",
                    FirstName =  "Andrey",
                    LastName = "Mitsyk",
                    Password = "1234"
                });
                db.SaveChanges();
            }
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
    }
}