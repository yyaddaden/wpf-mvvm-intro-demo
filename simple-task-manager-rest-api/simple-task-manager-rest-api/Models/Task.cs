using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_task_manager_rest_api.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}
