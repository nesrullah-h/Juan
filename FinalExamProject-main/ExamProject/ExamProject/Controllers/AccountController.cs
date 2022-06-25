using Core.Models;
using Data.DAL;
using ExamProject.Services.EmailServices;
using ExamProject.ViewModel.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ExamProject.Controllers
{
   
        public class AccountController : Controller
        {
            private readonly AppDbContext _context;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly UserManager<AppUser> _userManager;
            private readonly IOptions<MailSettings> _mailSettings;

            public AccountController(AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IOptions<MailSettings> mailSettings)
            {
                _context = context;
                _signInManager = signInManager;
                _userManager = userManager;
                _mailSettings = mailSettings;
            }
            // GET: AccountController
            public ActionResult Index()
            {
                return View();
            }

            // GET: AccountController/Details/5
            public ActionResult Details(int id)
            {
                return View();
            }

            // GET: AccountController/Register
            public ActionResult Register()
            {
                return View();
            }

            // POST: AccountController/Register
            [HttpPost]
            [ValidateAntiForgeryToken]

            //mail:tu201906140@code.edu.az
            //password:Tural123@
            public async Task<ActionResult> Register(RegisterVM register)
            {
                try
                {
                    if (!ModelState.IsValid) return View();

                    AppUser newUser = new AppUser
                    {
                        Email = register.Email,
                        UserName = register.UserName

                    };
                    var result = await _userManager.CreateAsync(newUser, register.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    var token = _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    string confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { email = newUser.Email, token }, HttpContext.Request.Scheme, Request.Host.ToString());
                    Email.SendMail(_mailSettings.Value.Email, newUser.Email, _mailSettings.Value.Password, confirmationLink, "Confirm Email");
                      await _userManager.AddToRoleAsync(newUser, "Admin");
                return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            public async Task<ActionResult> ConfirmEmail(string email, string token)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return View("Error");
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    user.IsActivated = true;
                    await _context.SaveChangesAsync();
                }
                return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
            }
            // GET: AccountController/Edit/5
            public async Task<ActionResult> logOut(int id)
            {
            await _signInManager.SignOutAsync();
                return View();
            }

            // POST: AccountController/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, IFormCollection collection)
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

            // GET: AccountController/Delete/5
            public ActionResult LogIn(int id)
            {
                return View();
            }

            // POST: AccountController/Delete/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> LogIn(LoginVM login)
            {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null)
            {
                return NotFound();
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            //return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            return  RedirectToAction("Index","Dashboard");
            }
        }
    }
