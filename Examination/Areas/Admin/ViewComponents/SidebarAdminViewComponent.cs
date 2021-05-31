using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Examination.Areas.Admin.Common;
using Examination.Areas.Admin.Models;
using System.Security.Claims;
using Examination.Areas.Admin.Common.Extensions;
using System;
using Examination.Data;

namespace AdminLTE.ViewComponents
{
    
    public class SidebarAdminViewComponent : ViewComponent
    {
        private readonly ExaminationContext _context;
        public SidebarAdminViewComponent(ExaminationContext contex)
        {
            _context = contex;
        }

        public IViewComponentResult Invoke(string filter)
        {
         
            var sidebars = new List<SidebarMenu>();
            sidebars.Add(ModuleHelper.AddHeader("MAIN NAVIGATION"));

            sidebars.Add(ModuleHelper.AddTree("Umumta'lim bo'limi"));
            sidebars.Last().TreeChild = new List<SidebarMenu>()
                {
                    ModuleHelper.AddModule(ModuleHelper.Module.Home),
                    
                    ModuleHelper.AddModule(ModuleHelper.Module.Predmet),
                      ModuleHelper.AddModule(ModuleHelper.Module.Part),
                };
          
            sidebars.Add(ModuleHelper.AddTree("Administration"));
            sidebars.Last().TreeChild = new List<SidebarMenu>()
                {
                    ModuleHelper.AddModule(ModuleHelper.Module.SuperAdmin),
                    ModuleHelper.AddModule(ModuleHelper.Module.Role),
                };
                        sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.UserLogs));

            sidebars.Add(ModuleHelper.AddTree("Natijalar"));
            var m = _context.Predmets.Select(p => new SidebarMenu()

            {
                Type = SidebarMenuType.Link,
                Name = p.Nomi,
                IconClassName = "fa fa-link",
                URLPath = string.Format("/Admin/Result?PredmetsFilterApplied={0}", p.ID),

            });
            
             sidebars.Last().TreeChild = m.ToList();

         
            sidebars.Add(ModuleHelper.AddTree("Testni ko'chirish"));
            sidebars.Last().TreeChild = new List<SidebarMenu>()
                {
                    ModuleHelper.AddModule(ModuleHelper.Module.Localhost),
                    ModuleHelper.AddModule(ModuleHelper.Module.Site),
                };







            return View(sidebars);
        }
    }
}
