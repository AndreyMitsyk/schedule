namespace Scheduler.Models
{
    using System.Data.Entity;
    public class Db : DbContext
    {
        public Db()
            : base("DefaultConnection")
        {
        }

        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<DayOfWeekItem> DayOfWeekItems { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<LessonTime> LessonTimes { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ScheduleItem> ScheduleItems { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}