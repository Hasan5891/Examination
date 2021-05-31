using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.TestsSchools.Models
{
    public class TestResulGroupModel
    {
        [BindProperty]
        public IList<Useritem> Users { get; set; }


        public string Predmet { get; set; }
        public IEnumerable<SelectListItem> Predmets { get; set; }
        public IEnumerable<SelectListItem> Parts { get; set; }
        public IEnumerable<SelectListItem> Tests { get; set; }

        public int? PredmetsFilterApplied { get; set; }
        public int? PartsFilterApplied { get; set; }
        public int? TestsFilterApplied { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
       
    }
}
