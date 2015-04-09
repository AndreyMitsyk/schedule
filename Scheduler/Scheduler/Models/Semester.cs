using System;

namespace Scheduler.Models
{
    class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginOfSemester { get; set; }
        public DateTime EndOfSemester { get; set; }
    }
}
