using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using pesmissionbase.Models;
using pesmissionbase.Models.Dto;
using pesmissionbase.Models.Dto.Roles;
using pesmissionbase.Services.AddPermissions;
using pesmissionbase.Services.GetCategory;

namespace pesmissionbase.Controllers
{
    [AllowAnonymous]
    public class AccountsController : Controller
    {
      
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IAddPermission _addPermission;
        private readonly IGetCategory _getCategory;
        public AccountsController(UserManager<Users> userManager, SignInManager<Users> signInManager, RoleManager<Roles> roleManager, IAddPermission addPermission, IGetCategory getCategory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _addPermission = addPermission;
            _getCategory = getCategory;
        }
        public IActionResult Index()
        {

            return View();
        }


        public IActionResult Regester()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Regester(RegisterDto register)
        {

            Users user = new Users()
            {

                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.Email,

            };
            var resulat = _userManager.CreateAsync(user, register.Password).Result;
            if (resulat.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            string message = "";
            foreach (var item in resulat.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(register);
        }
        [HttpGet]
        public IActionResult login(string returnUrl = "/")
        {
            return View(new loginDto { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public IActionResult login(loginDto login)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userManager.FindByNameAsync(login.UserName).Result;

            _signInManager.SignOutAsync();

            var resualt = _signInManager.PasswordSignInAsync(user, login.Password, login.IsPersistent, true).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            var permissions = _roleManager.Roles.Include(x => x.UsersPermisions);
            if (resualt.Succeeded)
            {

                return Redirect(login.ReturnUrl);

            }
            return View();
        }

        public IActionResult logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddUserRole(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            var roles = new List<SelectListItem>(
                _roleManager.Roles.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Name
                }

                ).ToList());

            return View(new AddUserRoleDto
            {
                Id = Id,
                Roles = roles,
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}"

            });
        }
        [HttpPost]
        public IActionResult AddUserRole(AddUserRoleDto UserRole)
        {
            var user = _userManager.FindByIdAsync(UserRole.Id).Result;
            var resulat = _userManager.AddToRoleAsync(user, UserRole.Role).Result;

            return RedirectToAction("UerRoles", "Accounts", new { Id = user.Id });
        }
        public IActionResult UerRoles(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            ViewBag.UserInfo = $"Name : {user.FirstName } {user.LastName} Email:{user.Email}";
            return View(roles);
        }



        public IActionResult AddPermission(string Id)
        {
            _addPermission.Excute(new PerMissionsForAdd
            {
                ActionName = "CreateRole",
                ControllerName = "Role",
                IsAllowed = true,
                RoleId = Id

            });


            return View();
        }






        public IActionResult RolesPermission()
        {


            return View();
        }



    }





}
