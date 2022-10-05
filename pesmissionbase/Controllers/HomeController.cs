using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;
using pesmissionbase.Models;
using pesmissionbase.Services.AddCategory;
using pesmissionbase.Services.GetCategory;
using pesmissionbase.Services.IChangePlace;
using pesmissionbase.Services.IRemoveNode;
using pesmissionbase.Services.IRenameNode;
using System.Diagnostics;



namespace pesmissionbase.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
      
        private readonly ILogger<HomeController> _logger;
        private readonly IGetCategory _getCategory;
        private readonly IChangePlace _changePlace;
        private readonly IRenameNode _renameNode;
        private readonly IAddCategory _addCategory;
        private readonly IRemoveNode _removeNode;
        public HomeController(ILogger<HomeController> logger, IGetCategory getCategory, IChangePlace changePlace, IRenameNode renameNode, IAddCategory addCategory, IRemoveNode removeNode)
        {
            _logger = logger;
            _getCategory = getCategory;
            _changePlace = changePlace;
            _renameNode = renameNode;
            _addCategory = addCategory;
            _removeNode = removeNode;
        }

        public IActionResult Index()
        {
            var a = _getCategory.Excute().Data;
            return View(a);
        }
      
        [HttpGet]
        public IActionResult About()
        {


           


            return View();
        }


        [HttpPost]
        public IActionResult About(long host,long changeNode,string hitmode,long PLaceAfter,long PlaceBefore)
        {
       
            var resualt = _changePlace.Excute(new ChangeRequsetDto
            {
                HostId = host,
                ChangingnodeId = changeNode,
                Hitmode = hitmode,
                onlevelAfter = PLaceAfter,
                onlevelBfore=PlaceBefore,
            });

            return Json(resualt);
        }





        [HttpPost]
        public IActionResult About2(long NodeId, string NewName,string Nodetitle)
        {
            var resualt = _renameNode.Excute(new RequsetReNameNodeDto
            {
                NewName = NewName,
                NodeID = NodeId,
                Nodetitle= Nodetitle
            });

            return Json(resualt);
        }//for rename(edit node)

        [HttpPost]
        public IActionResult About3( string NewName,string Nodetitle, long ParentId)
        {
           
            var resualt = _addCategory.Excute(new RequsestAddDto
            {
                link = Nodetitle,
                Name = NewName,
                ParenetId = ParentId,
            });

            return Json(resualt);
        }//for rename(edit node)

        [HttpPost]
        public IActionResult About4(long NodeId)
        {
            
            var resualt = _removeNode.Excute(new RequsetRemoveNodeDto
            {
                Id = NodeId,
            });
            return Json((resualt));
        }





        //https://localhost:7072/Home/Privacy
        public IActionResult Privacy()
        {
            //JsonSerializerOptions options = new()
            //{
            //    ReferenceHandler = ReferenceHandler.IgnoreCycles,
            //    WriteIndented = true
            //};
            var a = _getCategory.Excute().Data;



            //string s = JsonSerializer.Serialize(a, options);



            var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();

            jsonResolver.RenameProperty(typeof(Grouping), "Name", "title");
            jsonResolver.RenameProperty(typeof(Grouping), "Childs", "children");
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var json = JsonConvert.SerializeObject(a,serializerSettings);
            
          
            return Json(JsonConvert.DeserializeObject(json));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
