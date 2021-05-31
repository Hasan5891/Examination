using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examination.Data
{
    public class Test
    {

        public int ID { get; set; }
        public string Nomi { get; set; }
       
        public int PartID { get; set; }
        public Part Part { get; set; }
        public bool isactive { get; set; }
        public ICollection<UserExams> UserExams { get; set; }
        public ICollection<Question> Questions { get; set; }
        public string UserID { get; set; }

        public string Description { get; set; }

    }
}