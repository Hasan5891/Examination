using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Data
{
    public class UserExams
    {
        public int ID { get; set; }
       
        public string UserName { get; set; }
        public int? TestID { get; set; }
        public Test Test { get; set; }
       public ICollection<AnsweredQuesttions> AnsweredQuesttions { get; set; }

    }
}
