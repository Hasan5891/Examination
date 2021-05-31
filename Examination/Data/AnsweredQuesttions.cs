using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Data
{
    public class AnsweredQuesttions
    {
        public int ID { get; set; }
        public int? AnswerID { get; set; }
        public Answer Answer { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }
        public int UserExamsID { get; set; }
        public UserExams  UserExams { get; set; }
       

    }
}
