using flights.Areas.Admin.Models;
using flights.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace flights.Areas.Admin.Controllers
{
   
    

    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]

    public class AdminController : Controller
    {
        private  RoleManager<IdentityRole> roleManger;
        public AdminController(RoleManager<IdentityRole> roleManager) {
        this.roleManger =roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectRole projectRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = projectRole.RoleName
                
                };
                IdentityResult result = await roleManger.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return View();
                }
                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
            }
        
            return View();
        }
    }
}
