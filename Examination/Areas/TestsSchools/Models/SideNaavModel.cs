using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.TestsSchools.Models
{
    public class SideNaavModel
    {
        public IEnumerable<Predmet1> Predmets { get; set; }
        public IEnumerable<APartNav> AParts { get; set; }
      

    }

    public class Predmet1
    {
        public IEnumerable<Part1> Parts { get; set; }
        public int ID { get; set; }
        public string Nomi { get; set; }


    }
    public class Part1
    {
        public int ID { get; set; }
        public string Nomi { get; set; }

    }
    public class APartNav
    {
        public int ID { get; set; }
        public string Nomi { get; set; }

    }
}

