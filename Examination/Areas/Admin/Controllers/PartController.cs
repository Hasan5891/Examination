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
  
    public class PartController : BaseController
    {
        private readonly ExaminationContext _context;

        public PartController(ExaminationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

           // ViewBag. = new SelectList(_context.Parts.ToList(), "ID", "Nomi");
            AddPageHeader("Dashboard", "");
            return View(await _context.Parts.Include(p=>p.Predmet).ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nomi,PredmetID,ID")] Part part)
        {
            if (ModelState.IsValid)
            {
                if (part.ID > 0)
                {
                    _context.Update(part);

                }
                else
                {
                    _context.Add(part);


                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        public async Task<IActionResult> Create(int? id)
        {

            var predmet = new Part();
          
            if (id.HasValue)
            {
                predmet = await _context.Parts.FirstOrDefaultAsync(m => m.ID == id);

            
             
            }
            ViewBag.Predmets = new SelectList(_context.Predmets, "ID", "Nomi", predmet.PredmetID);

            return PartialView("_ModalPart", predmet);




        }

        public async Task<IActionResult> DeletesPart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Parts.Include(p=>p.Predmet)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (part.ID == 0)
            {
                return NotFound();
            }

            return PartialView("_ModalDeletePart", part);
        }


        [HttpPost]
        public async Task<IActionResult> DeletesPart([Bind("ID")] Part part)
        {

            if (part != null)
            {


                var rempart = await _context.Parts.FirstAsync(p => p.ID == part.ID);

                _context.Parts.Remove(rempart);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }


            return NotFound();
        }

    }
}