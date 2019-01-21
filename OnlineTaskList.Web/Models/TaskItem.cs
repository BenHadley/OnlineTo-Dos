using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OnlineTaskList.Web.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        [DisplayName("Last Updated")]
        public DateTime LastUpdated { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Completed?")]
        public bool IsComplete { get; set; }
    }
}