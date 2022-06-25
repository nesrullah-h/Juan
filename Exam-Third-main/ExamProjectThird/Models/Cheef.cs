using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.Models
{
    public class Cheef
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public List<SocialMedia> socialMedias { get; set; }
        public bool IsDeleted { get; set; }
    }
}
