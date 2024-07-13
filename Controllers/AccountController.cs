using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mongomvc.Models;
using System.ComponentModel.DataAnnotations;

namespace mongomvc.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<applicationUser> _UserManager;
        private SignInManager<applicationUser> _signInManager;

        public AccountController(UserManager<applicationUser> UserManager, SignInManager<applicationUser> signInManager)
        {
            _UserManager = UserManager;
            _signInManager = signInManager;

        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required][EmailAddress] string Email, [Required] string Password,string? returnurl)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = await _UserManager.FindByEmailAsync(Email);
                if (applicationUser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(applicationUser, Password, false, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnurl) && Url.IsLocalUrl(returnurl))
                        {
                            return RedirectToAction(returnurl ?? "/");

                        }
                        else
                        {
                            return RedirectToAction("Index", "Secured");
                        }
                    }
                }
                ModelState.AddModelError(nameof(Email), "Invalid login attempt.");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
        await   _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
