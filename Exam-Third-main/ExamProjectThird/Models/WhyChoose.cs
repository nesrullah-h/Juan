using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.Models
{
    public class WhyChoose
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Title { get; set; }
        [Required,MaxLength(255)]
        public string Description{ get; set; }
        public bool IsDeleted { get; set; }
    }
}
