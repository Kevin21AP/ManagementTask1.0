using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTask.Entity
{
    public class Tasks
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string Priority { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
