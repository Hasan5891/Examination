using Examination.Areas.TestsSchools.Models;
using Examination.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.ViewComponents
{
    public class SidenavViewComponent : ViewComponent
    {
        private readonly ExaminationContext _context;
        public SidenavViewComponent(ExaminationContext contex)
        {
            _context= contex;
        }

        public IViewComponentResult Invoke(string filter)
        {
            var vm = new SideNaavModel()
            {
                Predmets = _context.Predmets.Select(p => new Predmet1()
                {

                    ID = p.ID,
                    Nomi = p.Nomi,
                    Parts = p.Parts.Select(q => new Part1()
                    {
                        ID = q.ID,
                        Nomi = q.Nomi
                    })

                }),         



            };
            
            return View(vm);
        }
    }
}
