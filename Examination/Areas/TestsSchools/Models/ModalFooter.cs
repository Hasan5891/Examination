using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.TestsSchools.Models
{
    public class ModalFooter
    {
        public string SubmitButtonText { get; set; } = "Saqlash";
        public string CancelButtonText { get; set; } = "Bekor";
        public string SubmitButtonID { get; set; } = "btn-submit";
        public string CancelButtonID { get; set; } = "btn-cancel";
        public string classButton { get; set; } = "btn btn-primary";

        public bool OnlyCancelButton { get; set; }
    }
}
