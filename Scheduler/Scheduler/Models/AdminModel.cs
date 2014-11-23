using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scheduler.Models
{
    public class AdminModel
    {
        public AdminModel()
        {
            CreatedTime = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int WeekNumberId { get; set; }
        public WeekNumber WeekNumber { get; set; }

        public int DayOfWeekItemId { get; set; }
        public DayOfWeekItem DayOfWeekItem { get; set; }

        public ScheduleItem[] ScheduleItems { get; set; }
    }
}