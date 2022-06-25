using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuanSelfEdu.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stars { get; set; } = 3;
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
