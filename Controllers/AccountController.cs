using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RayaTask.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RayaTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signIn;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signIn = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(SignIn signIn)
        {
            var res = await _signIn.PasswordSignInAsync(signIn.UserName, signIn.Password, false, false);

            if (res.Succeeded)
            {
                return RedirectToAction("Index", "Employees");
            }
            ModelState.AddModelError("", "Wrong username or Password");
            return View(signIn);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signIn.SignOutAsync();
            return RedirectToAction("LogIn");
        }
        public IActionResult SignUp()
        {
            List<SelectListItem> selectLists = new List<SelectListItem>();
            var roles = _roleManager.Roles;
            foreach (var role in roles)
            {
                selectLists.Add(new SelectListItem { Text = role.Name, Value = role.Id });
            }
            ViewBag.SelectLists = selectLists;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp signUp)
        {
            var res = await _userManager.CreateAsync(new IdentityUser
            {
                Email = signUp.Email,
                UserName = signUp.Name
            }, signUp.Password);

            if (res.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signUp.Name);
                var role = await _roleManager.FindByIdAsync(signUp.role);
                var result = await _userManager.AddToRoleAsync(user, role.Name);

                await _signIn.SignInAsync(new IdentityUser
                {
                    Email = signUp.Email,
                    UserName = signUp.Name
                }, false);

                return RedirectToAction("Index", "Employees");
            }
            else
            {
                foreach (IdentityError error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                List<SelectListItem> selectLists = new List<SelectListItem>();
                var roles = _roleManager.Roles;
                foreach (var role in roles)
                {
                    selectLists.Add(new SelectListItem { Text = role.Name, Value = role.Id });
                }
                ViewBag.SelectLists = selectLists;

                return View(signUp);
            }
        }
    }
}
