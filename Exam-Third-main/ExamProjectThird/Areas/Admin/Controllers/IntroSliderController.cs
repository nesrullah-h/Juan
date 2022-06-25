using ExamProjectThird.DAL;
using ExamProjectThird.Models;
using ExamProjectThird.Utilities;
using ExamProjectThird.Utilities.FIle;
using ExamProjectThird.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IntroSliderController : Controller
    {
        private IWebHostEnvironment _env;
        private AppDbContext _context;
        public string _errorMessage { get; set; }

       

        public IntroSliderController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }
        // GET: IntroSliderController
        public async Task<IActionResult> Index()
        {
            var IntroSliders = await _context.IntroSliders.ToListAsync();
            return View(IntroSliders);
        }

        // GET: IntroSliderController/Details/5
        public IActionResult Details(int id)
        {

            return View();
        }

        // GET: IntroSliderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IntroSliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MultipleSliderVM multipleSlider)
        {
            try
            {
                if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();
                if (!CheckImageValid(multipleSlider.Photos))
                {
                    ModelState.AddModelError("Photos", _errorMessage);
                    return View();
                }
                foreach (var photo in multipleSlider.Photos)
                {
                    string fileName = await photo.SaveFileAysnc(_env.WebRootPath, "img");
                    IntroSlider slider = new IntroSlider
                    {
                        ImageUrl = fileName,
                        Description = multipleSlider.Description,
                        Title = multipleSlider.Title

                    };
                    await _context.IntroSliders.AddAsync(slider);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
             
        }

        public bool CheckImageValid(List<IFormFile> photos)
        {
            foreach (var photo in photos)
            {
                if (photos.Count > 5)
                {
                    _errorMessage = $"You can choose a maximum of 5 photo ";
                    return false;
                }

                if (!photo.CheckFileType("image/"))
                {
                    _errorMessage = $"{photo.FileName} must be image type";
                    return false;
                }
                if (!photo.CheckFileSize(3096))
                {
                    _errorMessage = $"{photo.FileName} size must be lest then 3096 kb";
                    return false;
                }

            }
            return true;
        }
        // GET: IntroSliderController/Update/5
        public ActionResult Update(int id)
        {
            return View();
        }

        // POST: IntroSliderController/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, IntroSlider introSlider)
        {
            try
            {
                if (id != introSlider.Id) return BadRequest();
                if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

                IntroSlider dbIntroSlider = await _context.IntroSliders.FindAsync(id);
                if (dbIntroSlider == null) return NotFound();

                if (!introSlider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError($"Photo", "File must be image type");
                    return View(dbIntroSlider);
                }
                if (!introSlider.Photo.CheckFileSize(4096))
                {
                    ModelState.AddModelError($"Photo", "File size must be lest than 4 kb");
                    return View(dbIntroSlider);
                }
                Helper.RemoveFile(_env.WebRootPath, "img", dbIntroSlider.ImageUrl);
                string newFileName = await introSlider.Photo.SaveFileAysnc(_env.WebRootPath, "img");
                dbIntroSlider.ImageUrl = newFileName;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IntroSliderController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            IntroSlider introSlider = await _context.IntroSliders.FindAsync(id);
            if (introSlider == null) return NotFound();
            Helper.RemoveFile(_env.WebRootPath, "img", introSlider.ImageUrl);
            _context.IntroSliders.Remove(introSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }

        // POST: IntroSliderController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
