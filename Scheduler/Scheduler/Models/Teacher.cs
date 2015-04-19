namespace Scheduler.Models
{
    using System.Collections.Generic;

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}