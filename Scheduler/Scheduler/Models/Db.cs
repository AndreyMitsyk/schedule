using System.Data.Entity;

namespace Scheduler.Models
{
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
        public DbSet<TeachSubj> TeachSubjs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        
    }
}