using System.Web.Mvc;
using Scheduler.Models;

namespace Scheduler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new Db())
            {
                db.Places.Add(new Place {Auditorium = "POpo"});
                db.SaveChanges();
            }
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
    }
}