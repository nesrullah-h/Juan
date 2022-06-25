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
    public class SellerController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public SellerController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Seller> ban = _context.Sellers.ToList();
            return View(ban);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Seller Seller = await _context.Sellers.FindAsync(id);
            if (Seller == null)
            {
                return NotFound();
            }
            _context.Sellers.Remove(Seller);
            Helper.DeleteImage(_webhost, "img/seller", Seller.Image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller Seller)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Seller.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Seller.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }

            string filename = await Seller.Photo.SaveImage(_webhost, "img/Seller");
            Seller db = new Seller();
            db.Image = filename;
            db.Name = Seller.Name;
            db.Price = Seller.Price;
            db.Stars= Seller.Stars;
            await _context.Sellers.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Seller Seller = await _context.Sellers.FindAsync(id);
            if (Seller == null) return NotFound();
            return View(Seller);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Seller Seller, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Seller.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Seller.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }
            Seller existtitle = _context.Sellers.FirstOrDefault(c => c.Name.ToLower() == Seller.Name.ToLower());
            Seller db = await _context.Sellers.FindAsync(id);
            if (existtitle != null)
            {
                if (db != existtitle)
                {
                    ModelState.AddModelError("Name", "Title Already Exist");
                    return View();
                }
            }
            if (db == null)
            {
                return NotFound();
            }

            string filename = await Seller.Photo.SaveImage(_webhost, "img/Seller");
            db.Image = filename;
            db.Name = Seller.Name;
            db.Price = Seller.Price;
            db.Stars = Seller.Stars;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Seller Seller = await _context.Sellers.FindAsync(id);
            if (Seller == null)
            {
                return NotFound();
            }
            return View(Seller);
        }
    }
}
