namespace Scheduler.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}