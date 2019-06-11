using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppMVC.Areas.Store.Models;
using AppMVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AppMVC.Areas.Store.Controllers
{
    [Area("Store")]
  //  [ValidateModel]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Items()
        {
            return View();
        }

        //[ValidateModel]
        public IActionResult AddUser(User user)
        {
            return Ok(user);
        }

        public IActionResult EditUser([Bind("Name", "Age", "HasRight")] User user)
        {
            string userInfo = $"Name: {user.Name}  Age: {user.Age}  HasRight: {user.HasRight}";
            return Content(userInfo);
        }

        public IActionResult GetUserAgent([FromHeader(Name = "content-type")] string userAgent)
        {
            return Content(userAgent);
        }

        [Route("[area]/[controller]/[action]/{Name}/{Age:int}/{Id:int}")]
        public IActionResult Test([FromRoute]User user)
        {
            return Ok(new { ModelState.IsValid, user });
        }

        public IActionResult TestDateTime(DateTime dt)
        {
            return Ok(new { ModelState.IsValid, dt });
        }

        public IActionResult TestCustomBind(Event ev)
        {
            return Ok(new { ModelState.IsValid, ev });
        }

       // [CustomExceptionFilter]
        public IActionResult TestException()
        {
            throw new Exception("TestException");
        }

        /*  public IActionResult DiTest([FromServices] TestServ testServ)
          {

              return Ok();
          }*/
    }
}