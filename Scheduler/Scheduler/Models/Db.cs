using System.Data.Entity;

namespace Scheduler.Models
{
    public class Db : DbContext
    {
        public Db()
            : base("DefaultConnection")
        {
        }

        public DbSet<ScheduleItem> ScheduleItems { get; set; }
        public DbSet<LessonTime> LessonTimes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<DayOfWeekItem> DayOfWeekItems { get; set; }
        public DbSet<WeekNumber> WeekNumbers { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}