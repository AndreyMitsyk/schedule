using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Schedule_model;

namespace schedule.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new Db())
            {
                db.Discipline.Add(new Discipline() {DisciplineName = "test"});
                db.Places.Add(new Place() {Auditorium = "DK-12"});
                db.Teacher.Add(new Teacher() {Name = "Willager 42"});
                db.Discipline_form.Add(new Discipline_form() {DisciplineFormName = "Лекция"});
                db.Group.Add(new Group() {Number = "3-42"});
                
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}