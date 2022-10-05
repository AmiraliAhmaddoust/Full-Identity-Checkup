using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pesmissionbase.Controllers
{


    public class test : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        public test(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        //  [Authorize("acsessAuthorizationHandler")]
        public IActionResult Index()
        {


            return View();


        }
    }
}
