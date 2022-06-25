using JuanSelfEdu.DAL;
using JuanSelfEdu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace JuanSelfEdu.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Slider = _context.Sliders.ToList();
            homeVM.Product = _context.Products.ToList();
            homeVM.Banner = _context.Banners.ToList();
            homeVM.Seller = _context.Sellers.ToList();
            homeVM.Blog = _context.Blogs.ToList();
            homeVM.Brand = _context.Brands.ToList();
            homeVM.Setting = _context.Setting.FirstOrDefault();


            return View(homeVM);
        }
    }
}
