using Examination.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.TestsSchools.Models
{
    public class TestViewModel
    {
      
        public ICollection<QuestionItem>Question { get; set; }

        public int? PartID { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
