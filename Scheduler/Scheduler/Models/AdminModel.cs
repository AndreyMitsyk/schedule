namespace Scheduler.Models
{
    public class AdminModel
    {
        public SearchModel Search { get; set; }
        public ScheduleItem[] Items { get; set; }
    }
}