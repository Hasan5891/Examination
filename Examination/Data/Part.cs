using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examination.Data
{
    public class Part
    {
        public int ID { get; set; }
        public string Nomi { get; set; }
        [Display(Name = "Predmet nomi")]
        public int PredmetID { get; set; }
        public Predmet Predmet { get; set; }
       
        public ICollection<Test> Tests { get; set; }



    }
}