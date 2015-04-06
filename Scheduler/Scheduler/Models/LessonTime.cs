namespace Scheduler.Models
{
    using System;

    public class LessonTime
    {
        public int Id { get; set; }
        public DateTime TimeOfBegin { get; set; }
        public DateTime TimeOfEnd { get; set; }
    }
}