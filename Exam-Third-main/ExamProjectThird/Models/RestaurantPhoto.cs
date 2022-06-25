using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.Models
{
    public class RestaurantPhoto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

    }
}
