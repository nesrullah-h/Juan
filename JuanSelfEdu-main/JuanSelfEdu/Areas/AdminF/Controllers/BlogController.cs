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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public BlogController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Blog> ban = _context.Blogs.ToList();
            return View(ban);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Blog Blog = await _context.Blogs.FindAsync(id);
            if (Blog == null)
            {
                return NotFound();
            }
            _context.Blogs.Remove(Blog);
            Helper.DeleteImage(_webhost, "img/blog", Blog.Image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog Blog)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Blog.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }

            string filename = await Blog.Photo.SaveImage(_webhost, "img/Blog");
            Blog db = new Blog();
            db.Image = filename;
            db.Name = Blog.Name;
            db.Title = Blog.Title;
            await _context.Blogs.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Blog Blog = await _context.Blogs.FindAsync(id);
            if (Blog == null) return NotFound();
            return View(Blog);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Blog Blog, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Blog.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }
            Blog existtitle = _context.Blogs.FirstOrDefault(c => c.Name.ToLower() == Blog.Name.ToLower());
            Blog db = await _context.Blogs.FindAsync(id);
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

            string filename = await Blog.Photo.SaveImage(_webhost, "img/Blog");
            db.Image = filename;
            db.Name = Blog.Name;
            db.Title = Blog.Title;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Blog Blog = await _context.Blogs.FindAsync(id);
            if (Blog == null)
            {
                return NotFound();
            }
            return View(Blog);
        }
    }
}
