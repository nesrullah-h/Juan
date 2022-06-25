using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.Models
{
    public class EventSlider
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
