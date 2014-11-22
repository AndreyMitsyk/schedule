using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule_model
{
    public class Db:DbContext
    {
        public Db():base("sa")
        {
            
        }
        public DbSet<Scheduler> Scheduler { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Time> Time { get; set; }
        public DbSet<Group> Group { get; set; }
    }
}
