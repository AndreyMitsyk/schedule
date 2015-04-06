namespace Scheduler.Models
{
    using System;

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int NumOfIncorrectEntery { get; set; }
        public DateTime LastEnterTry { get; set; }
    }
}