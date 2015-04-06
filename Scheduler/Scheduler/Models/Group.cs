namespace Scheduler.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public short YearOfReceipt { get; set; }
        public Faculty Faculty { get; set; }
    }
}