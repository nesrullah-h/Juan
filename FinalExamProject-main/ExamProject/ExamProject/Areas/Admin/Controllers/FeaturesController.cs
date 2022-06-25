using Core.Models;
using Data.DAL;
using ExamProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class FeaturesController : Controller
    {
        private readonly AppDbContext _context;

        public FeaturesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: FeaturesController
        [AllowAnonymous]

        public async Task<ActionResult> Index()
        {
            var fetures = await _context.Features.Where(f => !f.IsDeleted).ToListAsync();
            return View(fetures);
        }

        // GET: FeaturesController/Details/5
        [AllowAnonymous]

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeaturesController/Create
        [AllowAnonymous]

        public ActionResult Create()
        {
            return View();
        }

        // POST: FeaturesController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<ActionResult> Create(FeaturesVM featuresVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                Features features = new Features
                {
                    Title = featuresVM.Title,
                    Description = featuresVM.Description,
                    IconUrl = featuresVM.IconUrl
                };
                await _context.AddAsync(features);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FeaturesController/Edit/5
        [AllowAnonymous]

        public async Task<ActionResult> Update(int id)
        {
            var dbFeatures = await _context.Features.FindAsync(id);
            if (dbFeatures == null) return NotFound();
            return View(dbFeatures);
        }

        // POST: FeaturesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<ActionResult> Update(int id, Features features)
        {
     
                var dbFeatures = await _context.Features.FindAsync(id);
                if (dbFeatures == null) return NotFound();
            if (features.Title is null) dbFeatures.IconUrl = dbFeatures.IconUrl;
            if (features.Title is null) dbFeatures.Title = dbFeatures.Title;
            if (features.Title is null) dbFeatures.Description = dbFeatures.Description;

            dbFeatures.IconUrl = features.IconUrl;
            dbFeatures.Title = features.Title;
            dbFeatures.Description = features.Description;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                  
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
          
        }

        // GET: FeaturesController/Delete/5
        [AllowAnonymous]

        public async Task<ActionResult> Delete(int id)
        {
            var dbFeatures = await _context.Features.FindAsync(id);
            if (dbFeatures == null) return NotFound();
            dbFeatures.IsDeleted = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: FeaturesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
