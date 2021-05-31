using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examination.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Examination.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class CopyTestFromHostController : Controller
    {
        private readonly ExaminationContext _context;
        private readonly ExaminationContext1 _context1;
        private UserManager<ApplicationUser> userManager;
        public CopyTestFromHostController(ExaminationContext context, ExaminationContext1 context1, UserManager<ApplicationUser> userMgr)
        {
            _context = context;
            _context1 = context1;
            userManager = userMgr;
        }
        public async Task<IActionResult> Index()
        {

            var part = await _context.Parts.Include(p => p.Tests).ToListAsync();

            ViewBag.Parts = new SelectList(_context1.Parts.ToList(), "ID", "Nomi");
            ViewBag.Parts1 = new SelectList(_context.Parts.ToList(), "ID", "Nomi");

            return View();

        }

        public async Task<IActionResult> GetTest(int id)
        {

            var test = await _context.Tests.Include(q => q.Questions).Include(p => p.Part).ThenInclude(f => f.Predmet).Where(i => i.PartID == id).Select(s => new { Nomi = s.Nomi, predmet = s.Part.Predmet.Nomi, m = new { id = s.ID }, Savol = s.Questions.Count() }).ToListAsync();



            return Json(new { data = test });

        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, int partid)
        {
            var test = await _context.Tests.Include(q => q.Questions).ThenInclude(x => x.Answers).FirstAsync(t => t.ID == id);


            var part = await _context1.Parts.FindAsync(partid);


            var t1 = new Test
            {
                UserID = userManager.GetUserId(User),
                isactive = test.isactive,
                PartID = part.ID,
                Nomi = test.Nomi


            };
            _context1.Tests.Add(t1);

            t1.Questions = test.Questions.Select(s => new Question
            {
                TestID = t1.ID,
                Nomi = s.Nomi,
                Answers = s.Answers.Select(a => new Answer { isTrue = a.isTrue, Nomi = a.Nomi }).ToList()

            }).ToList();



            await _context1.SaveChangesAsync();


        

            return NoContent();



        }
        
    }
}