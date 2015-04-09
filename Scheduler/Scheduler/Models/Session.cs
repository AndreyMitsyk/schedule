using System;

namespace Scheduler.Models
{
    class Session
    {
        public int Id { get; set; }
        public DateTime StartSession { get; set; }
        public short Data { get; set; }
        public User User { get; set; }
    }
}
