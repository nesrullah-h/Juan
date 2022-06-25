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
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public BrandController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Brand> ban = _context.Brands.ToList();
            return View(ban);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Brand Brand = await _context.Brands.FindAsync(id);
            if (Brand == null)
            {
                return NotFound();
            }
            _context.Brands.Remove(Brand);
            Helper.DeleteImage(_webhost, "img/brand", Brand.Image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand Brand)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Brand.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Brand.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }

            string filename = await Brand.Photo.SaveImage(_webhost, "img/Brand");
            Brand db = new Brand();
            db.Image = filename;
            await _context.Brands.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Brand Brand = await _context.Brands.FindAsync(id);
            if (Brand == null) return NotFound();
            return View(Brand);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Brand Brand, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Brand.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Brand.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }
            Brand db = await _context.Brands.FindAsync(id);
           
            if (db == null)
            {
                return NotFound();
            }

            string filename = await Brand.Photo.SaveImage(_webhost, "img/brand");
            db.Image = filename;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Brand Brand = await _context.Brands.FindAsync(id);
            if (Brand == null)
            {
                return NotFound();
            }
            return View(Brand);
        }
    }
}
