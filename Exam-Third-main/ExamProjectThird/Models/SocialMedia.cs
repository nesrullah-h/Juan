using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfileUrl { get; set; }
        public Cheef  Cheef{ get; set; }
        public bool IsDeleted { get; set; }

    }
}
