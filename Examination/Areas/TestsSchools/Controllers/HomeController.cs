using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examination.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Examination.Service;
using Examination.Areas.TestsSchools.Models;
using Examination.Infrastructure;
using Examination.Service.Core;
using Examination.Extensions;

namespace Examination.Areas.TestsSchools.Controllers
{
    [Area("TestsSchools")]
    public class HomeController : Controller

    {
        private readonly ExaminationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICatalogService _catalogService;
       


        public HomeController(ICatalogService catalogService, /*ITestService testService,*/ ExaminationContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _catalogService = catalogService;
          
            _context = context;
        }

        // GET: TestsSchools/Grades
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(int? PartFilterApplied, int? PredmetsFilterApplied, int? page1)
        {
            var itemsPage = 4;
            var catalogTestModel = await _catalogService.GetCatalogItems(page1 ?? 0,
            itemsPage, PartFilterApplied, PredmetsFilterApplied);
            ViewBag.PartFilterApplied = PartFilterApplied;
            ViewBag.PredmetsFilterApplied = PredmetsFilterApplied;
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("Index", catalogTestModel);
            }
            return View(catalogTestModel);
        }

      
        public async Task<IActionResult> Begin(int TestID,string UserName)
        {
           
            var test = HttpContext.Session.GetJson<TestAnswerModel>("Test1");
            if (test != null ) {
             //   if (test.TestID == TestID)
               // {
                    // ViewData["Message"] = "Siz testdan o'tib bo'lgansiz";
                    return PartialView("Test", test);
                   
                 
                //}
            }

            var active = await _context.Tests.Include(p=>p.Part.Predmet).Include(l=>l.Questions).ThenInclude(a=>a.Answers).FirstOrDefaultAsync(i => i.ID == TestID);
            if(active == null){
              
                return new NotFoundResult();
            }
            if (!active.isactive)
            {
                 return new NotFoundResult();
                
            }
                       
            var items = active.Questions.Select(q => new QuestionItem()
            {
                QuestionID = q.ID,
                Nomi = q.Nomi,
                Answers = q.Answers.Select(c => new Answeritem()
                {
                    ID = c.ID,
                    Nomi = c.Nomi

                }).ToList().Shuffle()
            }).ToList().Shuffle();

            var total = items.Count();
                if (total > 0)
                {
                    var stExam = new UserExams
                    {
                        TestID = active.ID,
                       UserName= UserName
                    };
                    var starUser = await _context.UserExams.AddAsync(stExam);
                    await _context.SaveChangesAsync();
                TestAnswerModel model = new TestAnswerModel
                {
                    Questions = items,
                    ExamID = stExam.ID,
                    Predmet = active.Part.Predmet.Nomi,
                    FISH = UserName,
                    TestID = TestID
                };
                model.PaginationInfo = new PaginationInfoViewModel
                    {
                        ActualPage = 1,
                        TotalItems = total
                    };
                    HttpContext.Session.SetJson("Test1",model);

                    return PartialView("Test", model);
                }
            
            ViewData["Message"] = "Hech narsa topilmadi"; return PartialView("Message");


        }
        [HttpPost]
        public IActionResult Tester(int? QuestionID, int? AnswerID)
        {
           
            var items = HttpContext.Session.GetJson<TestAnswerModel>("Test1") ;
            if (AnswerID != null && QuestionID != null)
            {
                items.Questions[QuestionID.Value].AnswerID = AnswerID;
                HttpContext.Session.SetJson("Test1", items);
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }
        [HttpPost]
        public async Task<IActionResult> Test(IList<QuestionItem> Questions,int ExamID)
        {
            
            HttpContext.Session.Clear();
            if (Questions != null)
            {
                foreach (var item in Questions)
                {
                    await _context.AnsweredQuesttions.AddAsync(new AnsweredQuesttions
                    {
                        AnswerID = item.AnswerID,
                        QuestionID = item.QuestionID ?? 0,
                        UserExamsID = ExamID
                    });


                }
                await _context.SaveChangesAsync();
                ViewData["Message"] = "Bugungi ishtirokingiz g'oyatda muffaqiyatli o'tdi. Natijani bilish uchun Adminga murojat qiling";

                return PartialView("Telegram");
            }

            ViewData["Message"] = "Bugungi ishtirokingiz muffaqiyatsiz o'tdi.Keyinroq urinib ko'ring";

            return PartialView("Message");

        }
       

        [HttpGet]
        [Authorize(Policy = "readonly")]
        public async Task<IActionResult> Result(int? ExamID)
        {
            var n = await _context.UserExams.Include(p => p.Test).ThenInclude(q => q.Part).ThenInclude(r => r.Predmet).Include(q => q.AnsweredQuesttions).ThenInclude(q => q.Question).ThenInclude(q => q.Answers).FirstOrDefaultAsync(q => q.ID == ExamID);
            TestAnswerModel items = new TestAnswerModel()
            {
                ExamID = n.ID,
                Test = n.Test.Nomi,
                Predmet = n.Test.Part.Predmet.Nomi,
                FISH = n.UserName,
                T = (n.AnsweredQuesttions == null) ? 0 : n.AnsweredQuesttions.Where(p => p.Answer != null ? (p.Answer.isTrue) : false).Count(),
                Questions = n.AnsweredQuesttions.Select(p => new QuestionItem()
                {
                    Nomi = p.Question.Nomi,
                    Answers = p.Question.Answers.Select(m => new Answeritem() { ID = m.ID, Nomi = m.Nomi }).ToList(),
                    AnswerID = p.AnswerID,
                    QuestionID = p.QuestionID,
                    Answer = p.Answer
                }).ToList(),

            };
            GetResul(items);
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("Result", items);
            }
            return View(items);

        }
        private void GetResul(TestAnswerModel catalogTestModel)
        {
            var i = catalogTestModel.Foiz;
            if (i <= 55.00M)
            {
                catalogTestModel.Natija = "Qoniqarsiz";
                catalogTestModel.Natijalar = "red lighten-4";return;
            }

            else
            if (i <= 70.00M)
            {
                catalogTestModel.Natija = "Qoniqarli";
                catalogTestModel.Natijalar = "lime lighten-4"; return;
            }
            else
            if (i <= 85.00M)
            {
                catalogTestModel.Natija = "Yaxshi";
                catalogTestModel.Natijalar = "green lighten-4"; return;
            }
            else
            {
                catalogTestModel.Natija = "A'lo";
                catalogTestModel.Natijalar = "light-blue lighten-4"; return;

            }
        }

        
       
       
      
        public async Task<IActionResult> Details(int? id)
        {
            var test = HttpContext.Session.GetJson<TestAnswerModel>("Test1"); 
            if (test != null)
            {
              return View("Test", test);
            
            }
            if (id != null)
            {
                var item = await _context.Tests.Include(p => p.Part).ThenInclude(f => f.Predmet).Include(q => q.Questions).FirstOrDefaultAsync(a => a.ID == id&& a.isactive);
                if (item == null) { ViewData["Message"] = "Hech narsa topilmadi"; return View("Message"); }
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == item.UserID);
                var model = new TestDetailModel()
                {
                    TestID = item.ID,
                    TestName = item.Nomi,
                    TotalItems = item.Questions.Count(),
                    Description = item.Description,
                    PredmetName = item.Part.Predmet.Nomi,
                    UserName = user.UserName,
                    email = user.Email,
                    Foto = user.photo_url,
                    Phone = user.PhoneNumber,


                };
                ViewData["Title"] = model.PredmetName + " fanidan o'quvchilar uchun test savollari";
                if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("Details", model);
                }

                return View(model);
                
            }
            return new NotFoundResult();

        }
    }
}
