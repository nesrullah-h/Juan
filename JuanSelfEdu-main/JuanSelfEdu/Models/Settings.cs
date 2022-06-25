using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuanSelfEdu.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Insta { get; set; }
        public string Twitter { get; set; }
        public string Face { get; set; }
        public string Linkedin { get; set; }
        public string Location { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }


    }
}
