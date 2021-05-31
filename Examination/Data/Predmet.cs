using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examination.Data
{
    public class Predmet
    {
    
        public int ID { get; set; }
        [Display(Name = "Predmet nomi")]
        public string Nomi { get; set; }
        public ICollection<Part> Parts { get; set; }
        
    }
}