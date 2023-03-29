using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TasksWebApi.Models
{
    public class CreateTaskDto
    {
        [Required]
        [MaxLength(25)]
        public string Title { get; set; }
        
        public string Description { get; set; }
        [Required]
        public DateTime? ExpiredDate { get; set; }
        public DateTime CreatingDate { get; set; }
        public bool IsDone { get; set; }
    }
}
