using EduHomeBackendim.Extensions;
using EduHomeBackendim.Helpers;
using JuanSelfEdu.DAL;
using JuanSelfEdu.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace JuanSelfEdu.Areas.AdminF.Controllers
{
   
    [Area("AdminF")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> ban = _context.Products.ToList();
            return View(ban);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product Product = await _context.Products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(Product);
            Helper.DeleteImage(_webhost, "img/product", Product.Image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product Product)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Product.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Product.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }

            string filename = await Product.Photo.SaveImage(_webhost, "img/product");
            Product db = new Product();
            db.Image = filename;
            db.Title = Product.Title;
            db.Price = Product.Price;
            await _context.Products.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product Product = await _context.Products.FindAsync(id);
            if (Product == null) return NotFound();
            return View(Product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Product Product, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Product.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Product.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }
            Product existtitle = _context.Products.FirstOrDefault(c => c.Title.ToLower() == Product.Title.ToLower());
            Product db = await _context.Products.FindAsync(id);
            if (existtitle != null)
            {
                if (db != existtitle)
                {
                    ModelState.AddModelError("Title", "Title Already Exist");
                    return View();
                }
            }
            if (db == null)
            {
                return NotFound();
            }

            string filename = await Product.Photo.SaveImage(_webhost, "img/product");
            db.Image = filename;
            db.Title = Product.Title;
            db.Price = Product.Price;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product Product = await _context.Products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }
    }
}
