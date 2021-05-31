using Examination.Areas.Admin.Common.Attributes;
using Examination.Areas.Admin.Models;
using Examination.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Examination.Areas.Admin.Controllers
{
          [Area("Admin")]
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ExaminationContext _context;
        private UserManager<ApplicationUser> userManager;

        public HomeController(ExaminationContext context, UserManager<ApplicationUser> userMgr)
        {
            _context = context;
            userManager = userMgr;

        }
      
        public async Task<IActionResult> Index()
        {

            ViewBag.Parts = new SelectList(_context.Parts.ToList(), "ID", "Nomi");
            AddPageHeader("Dashboard", "");
         
            return View(await _context.Tests.Include(p => p.Questions).Include(p => p.Part).ThenInclude(p => p.Predmet).Include(p=>p.UserExams).ToListAsync());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nomi,PartID,Description")] Test test)
        {
            if (ModelState.IsValid)
            {
                test.UserID = userManager.GetUserId(User);
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }

        [HttpPost]
        public IActionResult Index(object model)
        {
            AddPageAlerts(PageAlertType.Info, "you may view the summary <a href='#'>here</a>");
            return View("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.Include(p => p.Part).ThenInclude(q => q.Predmet).FirstOrDefaultAsync(p => p.ID == id);

            ViewBag.Parts = new SelectList(_context.Parts.ToList(), "ID", "Nomi", test.PartID);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nomi,PartID,isactive,Description")] Test test)
        {
            if (id != test.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    test.UserID = userManager.GetUserId(User);
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartExists(test.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }

        [HelpDefinition]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            AddBreadcrumb("About", "/Account/About");

            return View();
        }

        [HelpDefinition("helpdefault")]
        public IActionResult Contact()
        {
            AddBreadcrumb("Register", "/Account/Register");
            AddBreadcrumb("Contact", "/Account/Contact");
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> GetQuestions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.Where(p => p.TestID == id).ToListAsync();



            if (question == null)
            {
                return NotFound();
            }

            return Json(new { data = question });
        }
        [ActionName("EditQuestion")]
        public async Task<IActionResult> EditQuestion(int? id, int? TestID)
        {

            var question = new Question();
            if (id.HasValue)
            {
                question = await _context.Questions.Include(p => p.Test).Include(r => r.Answers).FirstOrDefaultAsync(m => m.ID == id);

                return PartialView("FormLine", question);
            }
            question.Test = await _context.Tests.SingleAsync(p => p.ID == TestID);
            return PartialView("FormLine", question);



           
        }
        [HttpPost]
        [ActionName("EditQuestion")]
        public async Task<IActionResult> EditQuestion1([Bind("ID,Nomi,TestID,Answers")] Question question)
        {



            if (question.ID > 0)
            {


                _context.Update(question);
            }
            else
            {


                _context.Add(question);
            }
            await _context.SaveChangesAsync();






            return RedirectToAction(nameof(Edit), new { id = question.TestID });
        }

        public IActionResult Add(int? Index)
        {
            return PartialView("Addquestion", Index);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAnswer(int? ID)
        {

            if (ID != null)
            {
                var remove = await _context.Answers.FindAsync(ID);

                if (remove != null)
                {
                    _context.Answers.Remove(remove);
                    await _context.SaveChangesAsync();

                    return Json(new { status = true });
                }
            }
            return Json(new { status = false });

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuestions([Bind("ID,PartID")] Question question)
        {


            if (question != null)
            {

                var TestID = question.TestID;
                var remquestion = await _context.Questions.Include(p => p.Answers).FirstAsync(p => p.ID == question.ID);
                _context.Questions.Remove(remquestion);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Edit), new { id = TestID });

            }


            return NotFound();
        }

        [ActionName("DeleteQuestions")]
        public async Task<IActionResult> Deletes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (question.ID == 0)
            {
                return NotFound();
            }

            return PartialView("_ModalDelete", question);
        }
        [ActionName("DeletesTest")]
        public async Task<IActionResult> DeletesTest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .FirstOrDefaultAsync(m => m.ID == id);
            if (test.ID == 0)
            {
                return NotFound();
            }

            return PartialView("_ModalDeleteTest", test);
        }
        [HttpPost, ActionName("DeletesTest")]
        public async Task<IActionResult> DeletesTest([Bind("ID")] Test test)
        {

            if (test != null)
            {
               
                var remtest = await _context.Tests.Include(u=>u.UserExams).FirstAsync(p => p.ID == test.ID);
               
                _context.Tests.Remove(remtest);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }


            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(part);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteExam(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.FindAsync(id);
            if (test== null)
            {
                return NotFound();
            }

            return PartialView("_ModalDeleteExam", test);
        }
        [HttpPost, ActionName("DeleteExam")]
        public async Task<IActionResult> DeleteExamPost([Bind("ID")] Test test)
        {
            
           
         
              var exams =  _context.UserExams.Include(a=>a.AnsweredQuesttions).Where(p =>p.TestID== test.ID);
            _context.UserExams.RemoveRange(exams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PartExists(int id)
        {
            return _context.Tests.Any(e => e.ID == id);
        }

        public IActionResult Error()
        {
            return View();
        }

       
       

        
    }
}
