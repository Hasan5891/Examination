using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.TestsSchools.Models
{
    public class TestDetailModel
    {
        public string UserName { get; set; }
        public string Foto { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }
        public string TestName { get; set; }
        public string PredmetName { get; set; }
        public int TotalItems { get; set; }
        public string Description { get; set; }
        public int TestID { get; set; }
        public decimal Price { get; set; }
               
    }
}
