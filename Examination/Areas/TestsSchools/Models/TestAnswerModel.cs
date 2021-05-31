using Examination.Areas.TestsSchools.Models;
using Examination.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.TestsSchools.Models
{
    public class TestAnswerModel
    {
        [BindProperty]
        public IList<QuestionItem> Questions { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }

        public int ExamID { get; set; }
        public string FISH { get; set; }
        public string Predmet { get; set; }
        public string Test { get; set; }
        public int TestID { get; set; }
        public  int Soni { get { return ((Questions == null) ? 0 : Questions.Count()); } }
        public int state { get; set; }
        public int T { get; set; }
        public int N { get { return Soni - T; } }
        public decimal Foiz { get { return (Soni == 0) ? 0 : (((decimal)T / Soni) * 100.00M); } }
        public string Natija { get; set; }
        public string Natijalar { get; set; }

    }
    public class UserSession
    {
        public int TestID { get; set; }
    }
    }
