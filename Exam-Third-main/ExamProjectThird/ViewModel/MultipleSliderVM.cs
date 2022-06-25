using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.ViewModel
{
    public class MultipleSliderVM
    {
        public int Id { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
