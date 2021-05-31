using Examination.Areas.Admin.Common.Attributes;
using Examination.Areas.Admin.Models;
using Examination.Data;
using Microsoft.AspNetCore.Authorization;
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
   
    public class PredmetController : BaseController
    {
        private readonly ExaminationContext _context;

        public PredmetController(ExaminationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            AddBreadcrumb("About", "/Account/About");
          
            // ViewBag.Parts = new SelectList(_context.Parts.ToList(), "ID", "Nomi");
            AddPageHeader("Dashboard", "");
            return View(await _context.Predmets.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nomi")] Predmet predmet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(predmet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(predmet);
        }

        public async Task<IActionResult> Create(int? id)
        {

            var predmet = new Predmet();
            if (id.HasValue)
            {
                predmet = await _context.Predmets.FirstOrDefaultAsync(m => m.ID == id);

                return PartialView("_ModalPredmet", predmet);
            }
            
            return PartialView("_ModalPredmet", predmet);




        }
    
        public async Task<IActionResult> DeletesPredmet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predmet = await _context.Predmets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (predmet.ID == 0)
            {
                return NotFound();
            }

            return PartialView("_ModalDeletePredemt", predmet);
        }


        [HttpPost]
        public async Task<IActionResult> DeletesPredmet([Bind("ID")] Predmet predmet)
        {

            if (predmet != null)
            {


                var rempred = await _context.Predmets.FirstAsync(p => p.ID == predmet.ID);

                _context.Predmets.Remove(rempred);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }


            return NotFound();
        }

    }
}