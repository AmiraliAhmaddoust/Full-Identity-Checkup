using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using pesmissionbase.Data;
using pesmissionbase.Models;

namespace pesmissionbase.Helpers
{
    public class testRequirment : IAuthorizationRequirement
    {

    }

    public class acsessAuthorizationHandler : AuthorizationHandler<testRequirment>
    {
        private readonly RoleManager<Roles> _roleManager;
        private readonly UserManager<Users> _userManager;
        private readonly DataBaseContext _context;
        private readonly IMemoryCache _cache;
        public acsessAuthorizationHandler(RoleManager<Roles> roleManager, UserManager<Users> userManager, DataBaseContext context, IMemoryCache cache)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _cache = cache;

        } 
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, testRequirment requirement)
        {
            //  var resource = "1";

            //    var routeValues = context.Resource as HttpContext ;
            var routeValuess = context.Resource;
         
            var ad = routeValuess.GetType().GetProperty("RouteData").GetValue(routeValuess, null);

            var route = ad.GetType().GetProperty("Values").GetValue(ad, null) as RouteValueDictionary;

            object controller = null;
            route.TryGetValue("controller", out controller);
            object action = null;
            route.TryGetValue("action", out action);



            var controllerName = controller.ToString();
            var actionName = action.ToString();

            var Username = context.User.Identity.Name;
            if (Username == null)
            {
                return Task.CompletedTask;
            }
            //object Data;
            //if (!_cache.TryGetValue("roles",out Data))
            //{
            //    Data = _userManager.GetRolesAsync(_userManager.FindByNameAsync(Username).Result).Result;
            //    _cache.Set("roles", Data);
            //}
            //else
            //{
            //   Data= _cache.Get("roles");
            //}
            var roles = _userManager.GetRolesAsync(_userManager.FindByNameAsync(Username).Result).Result;

            // var r=_roleManager.Roles.Where(p=>p.Name.Equals(_userManager.GetRolesAsync(_userManager.FindByNameAsync(Username).Result).Result)).Select(p=>p.Id).ToList();

           // var rrr = _roleManager.Roles.Include(x => x.UsersPermisions).Where(x => roles.Contains(x.Name)).ToList();


            List<string> roleid = new List<string>();
            foreach (var itam in roles)
            {
                foreach (var item2 in _context.Roles)
                {
                    if (itam == item2.Name)
                    {
                        roleid.Add(item2.Id);
                    }
                }

            }



       //     var a = _context.Permissions.Where(p => p.ControlerName == controllerName && p.ActionName == actionName).Select(p => p.PermissionsId).FirstOrDefault();

            var guid = "0";
            foreach (var item in _context.Permissions)
            {
                if (item.ControlerName == controllerName && item.ActionName == actionName)
                {
                    guid = item.PermissionsId;
                    break;
                }
            }




            List<Roles> ob ;
            if (!_cache.TryGetValue("test", out ob))
            {
                ob = _context.Roles.ToList();
                _cache.Set("test", ob);
            }
            else
            {
                ob = _cache.Get("test") as List<Roles>;
            }
           

            foreach (var item in _context.UsersPermisions)
            {
                foreach (var item2 in roleid)
                {
                    if (item2 == item.RoleId && guid == item.PermissionId)
                    {
                        context.Succeed(requirement);
                        break;
                    }
                }
            }

            return Task.CompletedTask;
        }
    }

}
