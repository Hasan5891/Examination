using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Examination.Models;
using Microsoft.AspNetCore.Authorization;
using Examination.Data;
using Examination.Areas.TestsSchools.Models;
using Examination.Infrastructure;

namespace Examination.Controllers
{
  
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("Index");
            }
            var test = HttpContext.Session.GetJson<TestAnswerModel>("Test1");
            if (test != null)
            {
               
                return View("~/Areas/TestsSchools/Views/Shared/Test.cshtml",test);


                //}
            }

            return View();
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
