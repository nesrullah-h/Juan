using ExamProjectThird.DAL;
using ExamProjectThird.Models;
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
    public class WhyChooseController : Controller
    {
        private AppDbContext _context;

        public WhyChooseController(AppDbContext context)
        {
            _context = context;
        }
        // GET: WhyChooseController
        public async Task<ActionResult> Index()
        {
            var WhyChoose = await _context.WhyChoose.Where(w => w.IsDeleted == false).ToListAsync();
            return  View(WhyChoose);
        }

        // GET: WhyChooseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WhyChooseController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WhyChooseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WhyChoose  whyChoose)
        {
            try
            {
                if (!ModelState.IsValid) return View();             
                bool IsExist = _context.WhyChoose.Any(w => w.Title.Trim().ToLower() == whyChoose.Title.Trim().ToLower());
                if (IsExist)
                {
                    ModelState.AddModelError("Title", "This Name already exist");
                    return View();
                }
                await _context.AddAsync(whyChoose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));

            }
            catch
            {
                return View();
            }
        }

        // GET: WhyChooseController/Udate/5
        public IActionResult Update(int id)
        {
            WhyChoose whyChoose = _context.WhyChoose.Find(id);
            if (whyChoose == null) return NotFound();
            return View(whyChoose);
        }

        // POST: WhyChooseController/Udate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, WhyChoose whyChoose)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                if (id != whyChoose.Id) return BadRequest();
                WhyChoose dbWhyChoose = await _context.WhyChoose.Where(w => w.IsDeleted == false && w.Id == id).FirstOrDefaultAsync();
                if (dbWhyChoose.Title.ToLower().Trim() == whyChoose.Title.ToLower().Trim() && dbWhyChoose.Description.ToLower().Trim() == whyChoose.Description.ToLower().Trim())
                {
                    return RedirectToAction(nameof(Index));
                }
                bool IsExist = _context.WhyChoose
                              .Any(c => c.Title.ToLower().Trim() == whyChoose.Title.ToLower().Trim());
                if (IsExist)
                {
                    ModelState.AddModelError("Name", "This Title already exist");
                    return View(dbWhyChoose);
                }
                dbWhyChoose.Title = whyChoose.Title;
                dbWhyChoose.Description = whyChoose.Description;
                await _context.SaveChangesAsync();
          
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WhyChooseController/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
           
        //}

        // POST: WhyChooseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                WhyChoose whyChoose = await _context.WhyChoose.Where(w => w.IsDeleted == false && w.Id == id).FirstOrDefaultAsync();
                if (whyChoose == null) return NotFound();
                whyChoose.IsDeleted = true;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
