using ExamProjectThird.DAL;
using ExamProjectThird.Models;
using ExamProjectThird.ViewModel.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ExamProjectThird.Utilities.Helper;

namespace ExamProjectThird.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private AppDbContext _context;
        private RoleManager<IdentityRole> _roleManager;
        private IOptions<MailSettings> _mailSettings;
        private SignInManager<ApplicationUser> _sigInManager;

        public AccountController(UserManager<ApplicationUser> userManager, AppDbContext context,RoleManager<IdentityRole> roleManager, IOptions<MailSettings> mailSettings,SignInManager<ApplicationUser> signInManager)

        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _mailSettings = mailSettings;
            _sigInManager = signInManager;

        }
        // GET: AccountController
        public IActionResult Register()
        {
            return View();
        }
        #region

        //public async Task<IActionResult> rgst(RegisterVM register)
        //{
        //    if (!ModelState.IsValid) return View();
        //    ApplicationUser newUser = new ApplicationUser
        //    {
        //        FullName = register.FullName,
        //        Email = register.Email,
        //        UserName = register.UserName
        //    };
        //    IdentityResult result = await _userManager.CreateAsync(newUser, register.Pasword);
        //    string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
        //    string cfrmLInk = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token }, Request.Scheme, Request.Host.ToString());
        //    await _userManager.AddToRoleAsync(newUser, UserRoles.Admin.ToString());
        //}
        //public async Task<IActionResult> cnfrm(string token, string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null) return View("Error");
        //    var result = await _userManager.ConfirmEmailAsync(user, token);
          
        //    if (result.Succeeded)
        //    {
        //        user.IsActivated = true;
        //        await _context.SaveChangesAsync();
        //        return View(user);
        //    }
        //}
        #endregion
        // GET: AccountController/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            ApplicationUser newUser = new ApplicationUser
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.UserName

            };
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Pasword);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }

            TempData["Email"] = newUser.Email;
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token }, Request.Scheme,Request.Host.ToString());
            Email.SendMail(_mailSettings.Value.Mail, newUser.Email, confirmationLink, _mailSettings.Value.Password, "Email Confirmation");
            await _userManager.AddToRoleAsync(newUser, UserRoles.SuperAdmin.ToString());
           

            return RedirectToAction(nameof(SuccessRegistration));
        }

        // GET: AccountController/Create
        public ActionResult SuccessRegistration()
        {
            return View();
        }


        // POST: AccountController/Create
       
        // GET: AccountController/Edit/5
        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            
            
            if (result.Succeeded)
            {
                user.IsActivated = true;
                await _context.SaveChangesAsync();
                return View(user);
            }
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginVM login,string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid) return View(login);
                ApplicationUser user = await _userManager.FindByEmailAsync(login.Email);
                if (user==null)
                {
                    ModelState.AddModelError(String.Empty, "Email or Pasword is wrong");
                    return View(login);
                }
                if (!user.IsActivated)
                {
                    ModelState.AddModelError(String.Empty, "Please,Check yor mail and active account");
                    return View(login);
                }
                // var result = await _sigInManager.PasswordSignInAsync(user, userModel.Password, userModel.RememberMe, true);
                var result = await _sigInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(String.Empty, "Please,Wait a few moment");
                    return View(login);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(String.Empty, "Email or Pasword is wrong");
                    return View(login);
                }
                if (returnUrl!=null)
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
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
        #region CreateROle
        public async Task CreateRole()
        {
           
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }
        #endregion
    }
}
