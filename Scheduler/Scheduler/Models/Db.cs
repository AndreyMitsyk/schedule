using System.Data.Entity;

namespace Scheduler.Models
{
    public class Db : DbContext
    {
        public Db()
            : base("DefaultConnection")
        {
        }

        public DbSet<Scheduler> Scheduler { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<DisciplineForm> DisciplineForm { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Group> Group { get; set; }
    }
}