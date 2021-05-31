using Examination.Data;
using System.Collections.Generic;

namespace Examination.Areas.TestsSchools.Models
{
    public class Useritem
    {

        public int ExamID { get; set; }
        public string FISH { get; set; }
       
        public string TestNomi { get; set; }
        public string PredmetNomi { get; set; }
        public  int Soni { get; set; }
        public int T { get; set; }
        public int N { get { return Soni - T; } }
        public decimal Foiz { get { return (Soni == 0) ? 0 : (((decimal)T / Soni) * 100.00M); } }
      
        public string Natijalar {
            get
            {
                if (Foiz <= 55.00M)
                {
                    return "table-danger";

                }
                else
                      if (Foiz <= 70.00M)
                {

                    return "table-warning";



                }
                else
                        if (Foiz <= 85.00M)
                {

                    return "table-success";



                }
                else
                {
                    return "table-primary";


                }
            }
            set { Natijalar = value; }

        }
        public string Natija {  get {

                if (Foiz <= 55.00M)
                {
                    return "Qoniqarsiz";

                }
                else
                         if (Foiz <= 70.00M)
                {

                    return "Qoniqarli";



                }
                else
                           if (Foiz <= 85.00M)
                {

                    return "Yaxshi";



                }
                else
                {
                    return "A'lo";


                }


            } set { Natija = value; } }
        
        public IList<string> Answered { get; set; }

    }
   
}