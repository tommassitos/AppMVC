using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppMVC.Models;

namespace AppMVC.Controllers
{
    // [NonController]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [NonAction]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpGet]
        [HttpPost]
        [HttpPut]
        [HttpDelete]
        [HttpHead]
        [ActionName("Welcome")]
        public string Hello()
        {
            return "Hello ASP.NET";
        }

        public IActionResult Square(int altitude = 1, int height = 2)
        {
            double square = altitude * height / 2;
            return Content($"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}");
        }

        public JsonResult GetName()
        {
            string name = "Tom";
            return Json(name);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
