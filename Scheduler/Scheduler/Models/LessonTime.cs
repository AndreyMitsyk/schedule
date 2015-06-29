namespace Scheduler.Models
{
    using System;

    public class LessonTime
    {
        public int Id { get; set; }
        public TimeSpan TimeOfBegin { get; set; }
        public TimeSpan TimeOfEnd { get; set; }
    }
}