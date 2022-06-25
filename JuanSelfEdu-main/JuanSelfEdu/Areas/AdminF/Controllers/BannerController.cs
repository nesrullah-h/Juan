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
        public class BannerController : Controller
        {
            private readonly AppDbContext _context;
            private IWebHostEnvironment _webhost;
            public BannerController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
            {
                _context = context;
                _webhost = webHostEnvironment;
            }
            public IActionResult Index()
            {
                List<Banner> ban = _context.Banners.ToList();
                return View(ban);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                Banner Banner = await _context.Banners.FindAsync(id);
                if (Banner == null)
                {
                    return NotFound();
                }
                _context.Banners.Remove(Banner);
                Helper.DeleteImage(_webhost, "img/banner", Banner.Image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Banner Banner)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!Banner.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
                }
                if (Banner.Photo.CheckSize(8000))
                {
                    ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
                }

                string filename = await Banner.Photo.SaveImage(_webhost, "img/banner");
                Banner db = new Banner();
                db.Image = filename;
                db.Name = Banner.Name;
                db.Desc = Banner.Desc;
                await _context.Banners.AddAsync(db);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Update(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                Banner Banner = await _context.Banners.FindAsync(id);
                if (Banner == null) return NotFound();
                return View(Banner);
            }
            [HttpPost]
            public async Task<IActionResult> Update(Banner Banner, int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!Banner.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
                }
                if (Banner.Photo.CheckSize(8000))
                {
                    ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
                }
                Banner existtitle = _context.Banners.FirstOrDefault(c => c.Name.ToLower() == Banner.Name.ToLower());
                Banner db = await _context.Banners.FindAsync(id);
                if (existtitle != null)
                {
                    if (db != existtitle)
                    {
                        ModelState.AddModelError("Name", "Name Already Exist");
                        return View();
                    }
                }
                if (db == null)
                {
                    return NotFound();
                }

                string filename = await Banner.Photo.SaveImage(_webhost, "img/banner");
            db.Image = filename;
            db.Name = Banner.Name;
            db.Desc = Banner.Desc;
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }


            public async Task<IActionResult> Detail(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                Banner Banner = await _context.Banners.FindAsync(id);
                if (Banner == null)
                {
                    return NotFound();
                }
                return View(Banner);
            }
        }
    }
