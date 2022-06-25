using JuanSelfEdu.Models;
using System.Collections.Generic;

namespace JuanSelfEdu.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Slider { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<Banner> Banner { get; set; }
        public IEnumerable<Seller> Seller { get; set; }
        public IEnumerable<Blog> Blog { get; set; }
        public IEnumerable<Brand> Brand { get; set; }
        public Settings Setting { get; set; }


    }
}
