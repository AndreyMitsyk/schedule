using System;

namespace Scheduler.Models
{
    public class ScheduleItem
    {
        public ScheduleItem()
        {
            CreatedTime = DateTime.Now;
        }

        public long Id { get; set; }

        public DateTime CreatedTime { get; set; }
       
        public int? DayOfWeekItemId { get; set; }
        public DayOfWeekItem DayOfWeekItem { get; set; }

        public int? LessonTimeId { get; set; }
        public LessonTime LessonTime { get; set; }

        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int? LessonTypeId { get; set; }
        public LessonType LessonType { get; set; }

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int? AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }



        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int WeekNumberId { get; set; }
        public WeekNumber WeekNumber { get; set; }
    }
}