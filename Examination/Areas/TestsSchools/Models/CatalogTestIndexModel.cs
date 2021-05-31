using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.TestsSchools.Models
{
    public class CatalogTestIndexViewModel
    {
        public IEnumerable<CatalogTestitemViewModel> CatalogItems { get; set; }
        public IEnumerable<SelectListItem> Parts { get; set; }
      
        public int? PartFilterApplied { get; set; }
        public int? PredmetsFilterApplied { get; set; }
        public string Predmet { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }



    }
}
