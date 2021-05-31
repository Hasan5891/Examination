using Examination.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.TestsSchools.Models
{
    public class QuestionItem
    {
        public IList<Answeritem> Answers { get; set; }
        [BindProperty]
        public int? AnswerID { get; set; }
        [BindProperty]
        public int? QuestionID { get; set; }
        public int? AQuestionID { get; set; }
        //   public Question Questtion  { get; set; }
        public Answer Answer { get; set; }


        public string Nomi { get; set; }
        

    }
}
