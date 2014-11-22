using System.Linq;
using System.Web.Mvc;
using Antlr.Runtime;
using Scheduler.Models;

namespace Scheduler.Controllers
{
    public class HomeController : Controller
    {
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
    }
}