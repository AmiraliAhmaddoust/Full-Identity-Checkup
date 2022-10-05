using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pesmissionbase.Models;
using pesmissionbase.Models.Dto;
using pesmissionbase.Services.AddCategory;
using pesmissionbase.Services.GetCategory;

namespace pesmissionbase.Controllers
{
  //  [AllowAnonymous]
    public class AdminController1 : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IAddCategory _addCategory;
        private readonly IGetCategory _getCategory;
        public AdminController1(UserManager<Users> userManager, RoleManager<Roles> roleManager, IAddCategory addCategory, IGetCategory getCategory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _addCategory = addCategory;
            _getCategory = getCategory;

        }
        public IActionResult Index()
        {
            var users = _userManager.Users.Select(p => new UserListDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                UserName = p.UserName,
                PhoneNumber = p.PhoneNumber,
                EmailConfirmed = p.EmailConfirmed,
                AccessFailedCount = p.AccessFailedCount
            }).ToList();

            return View(users);
        }
        public IActionResult roles()
        {
            var roles = _roleManager.Roles.Select(p => new RoleListDto
            {
                Id = p.Id,
                RoleName = p.Name,
                Describtion = p.description
            }).ToList();
            return View(roles);
        }
        public IActionResult addNewUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addNewUser(RegisterDto register)
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
                return RedirectToAction("Index", "AdminController1");
            }
            string message = "";
            foreach (var item in resulat.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(resulat);
        }


        public IActionResult addCategorys()
        {

       //     ViewBag.Modal = _getCategory.Excute().Data; ;
            return View();
        }
        [HttpPost]
        public IActionResult addCategorys(RequsestAddDto addDto)
        {

            var ra = _addCategory.Excute(addDto);
            return Json(ra);
        }
    }
}
