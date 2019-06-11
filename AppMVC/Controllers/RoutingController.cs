using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AppMVC.Controllers
{
    [Route("[controller]")]
    [Route("main")]
    public class RoutingController : Controller
    {
        [Route("[action]/{name}")]
        [Route("store/{name}")]
        public IActionResult Index(string name)
        {
            return Content(name);
        }
        [Route("{id:int}/{name:maxlength(10)}")]
        public IActionResult Test(int id, string name)
        {
            return Content($" id={id} | name={name}");
        }

        //[Route("[controller]/[action]/{data}")] if controller attrs empty
        [Route("[action]/{data}")]
        public IActionResult Data(string data)
        {
            var controller = RouteData.Values["controller"].ToString();
            var action = RouteData.Values["action"].ToString();
            return Content($"controller: {controller} | action: {action}| data: {data}");
        }
    }
}