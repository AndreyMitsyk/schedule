using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scheduler.Models
{
    public class AdminModel
    {
        public SearchModel Search { get; set; }
        public ScheduleItem[] Items { get; set; }
    }
}