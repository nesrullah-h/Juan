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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public SettingController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Settings> ban = _context.Setting.ToList();
            return View(ban);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Settings Settings = await _context.Setting.FindAsync(id);
            if (Settings == null)
            {
                return NotFound();
            }
            _context.Setting.Remove(Settings);
            Helper.DeleteImage(_webhost, "images", Settings.Logo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Settings Settings)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Settings.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Settings.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }

            string filename = await Settings.Photo.SaveImage(_webhost, "images");
            Settings db = new Settings();
            db.Logo = filename;
            db.Insta = Settings.Insta;
            db.Twitter = Settings.Twitter;
            db.Face = Settings.Face;
            db.Linkedin = Settings.Linkedin;
            db.Location = Settings.Location;
            db.Mail = Settings.Mail;
            db.Phone = Settings.Phone;
            await _context.Setting.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Settings Settings = await _context.Setting.FindAsync(id);
            if (Settings == null) return NotFound();
            return View(Settings);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Settings Settings, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Settings.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Settings.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }
            Settings existtitle = _context.Setting.FirstOrDefault(c => c.Mail.ToLower() == Settings.Mail.ToLower());
            Settings db = await _context.Setting.FindAsync(id);
            if (existtitle != null)
            {
                if (db != existtitle)
                {
                    ModelState.AddModelError("Mail", "Title Already Exist");
                    return View();
                }
            }
            if (db == null)
            {
                return NotFound();
            }

            string filename = await Settings.Photo.SaveImage(_webhost, "img/Settings");
            db.Logo = filename;
            db.Insta = Settings.Insta;
            db.Twitter = Settings.Twitter;
            db.Face = Settings.Face;
            db.Linkedin = Settings.Linkedin;
            db.Location = Settings.Location;
            db.Mail = Settings.Mail;
            db.Phone = Settings.Phone;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Settings Settings = await _context.Setting.FindAsync(id);
            if (Settings == null)
            {
                return NotFound();
            }
            return View(Settings);
        }
    }
}
