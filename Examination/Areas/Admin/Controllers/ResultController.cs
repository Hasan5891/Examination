using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examination.Areas.TestsSchools.Models;
using Examination.Data;
using Examination.Service.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Examination.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class ResultController : Controller
    {
       
        private  ITestRerulService _testResultService;
            
        public ResultController( ITestRerulService testResultService)
        {
          _testResultService = testResultService;
        
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(int? PredmetsFilterApplied, int? PartsFilterApplied, int? TestsFilterApplied, int? page1)
        {
            var itemsPage = 100;
            var catalogTestModel = await _testResultService.GetCatalogItemsAdmin(page1 ?? 0,
                itemsPage, PredmetsFilterApplied, PartsFilterApplied, TestsFilterApplied);
           

            return View(catalogTestModel);
        }
      

        

    }
}