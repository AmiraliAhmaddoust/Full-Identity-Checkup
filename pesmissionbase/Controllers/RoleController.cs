using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pesmissionbase.Models;
using pesmissionbase.Models.Dto.Roles;

namespace pesmissionbase.Controllers
{
    [AllowAnonymous]
    public class RoleController : Controller
    {
        private readonly RoleManager<Roles> _roleManager;
        private readonly IAuthorizationService _authorizationService;
        public RoleController(RoleManager<Roles> roleManager, IAuthorizationService authorizationService)
        {
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }
        public IActionResult Index()
        {

            var resulat = _roleManager.Roles.Select(p => new RolesDto
            {
                Name = p.Name,
                Description = p.description,
                Id = p.Id,
            }).ToList();

            return View(resulat);
        }
        [HttpGet]

        public IActionResult CreateRole()
        {



            return View();

        }

        [HttpPost]
        public IActionResult CreateRole(AddRole newRole)
        {




            Roles newR = new Roles()
            {
                Name = newRole.Name,
                description = newRole.Description,
            };
            var resualt = _roleManager.CreateAsync(newR).Result;
            if (resualt.Succeeded)
            {
                return RedirectToAction("Index", "Role");
            };
            ViewBag.Errors = resualt.Errors.ToList();




            return View();
        }
    }
}
