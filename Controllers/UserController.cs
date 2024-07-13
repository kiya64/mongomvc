using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mongomvc.Models;
using mongomvc.Service;

namespace mongomvc.Controllers
{
    public class UserController : Controller
    {
        private UserManager<applicationUser> _UserManager;
        private RoleManager<applicationRole> _roleManager;
        private readonly StudentService _studentService;

        public UserController(UserManager<applicationUser> UserManager, RoleManager<applicationRole> roleManager, StudentService studentService)
        {
            _UserManager = UserManager;
            _roleManager = roleManager;
            _studentService = studentService;

        }
        public IActionResult CreatRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatRole(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new applicationRole
                {
                    Name = userRole.RoleName

                });
                if (result.Succeeded)
                {
                    ViewBag.ApplicationUser = "successfully";

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(userRole);

        }
        public IActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Creat(User user)
        {
            if (ModelState.IsValid) {
                applicationUser applicationUser = new applicationUser
                {
                    UserName = user.Name,
                    Email = user.Email,

                };
                IdentityResult result = await _UserManager.CreateAsync(applicationUser,user.Password);
                await  _UserManager.AddToRoleAsync(applicationUser, "Student");
              
                if (result.Succeeded)
                {
                    await _studentService.CreateAsync(new UserTableModel { UserName = user.Name });
                    ViewBag.ApplicationUser = "successfully";
                    return RedirectToAction("Index", "Home");



                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
            return View(user);
        }
    }
}
