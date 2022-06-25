using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuanSelfEdu.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
