using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksWebApi.Domains
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? ExpiredDate { get; set; }
        public DateTime CreatingDate { get; set; }
        public bool IsDone { get; set; }

        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }


    }
}
