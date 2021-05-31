using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examination.Data
{
    public class Question
    {
        public int ID { get; set; }
        [Display(Name = "Test savoli")]
        public string Nomi { get; set; }
        public IList<Answer> Answers { get; set; }
        [Display(Name = "Test Nomi")]
        public int TestID { get; set; }
        public Test Test { get; set; }
      
     


    }
}