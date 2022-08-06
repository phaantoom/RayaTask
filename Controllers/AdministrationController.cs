using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RayaTask.ViewModel;
using System.Threading.Tasks;

namespace RayaTask.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
                _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRole role)
        {
            var res = await _roleManager.CreateAsync(new IdentityRole { Name = role.Name });
            if (res.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach(var error in res.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(res);
        }
    }
}
