using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.Admin.Models
{
    public class PasswordModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Amaldagi parol kiritish talab etiladi")]
            [DataType(DataType.Password)]
            [Display(Name = "Amaldagi parol")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "Yangi parol kiritish talab etiladi")]
            [StringLength(100, ErrorMessage = " {0} maksimal {1} va minimal {2} simvol kiriting", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Yangi parol")]
            public string NewPassword { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Parolni tasdiqlash")]
            [Compare("NewPassword", ErrorMessage = "Yangi parol va parol tasdiqlash mos kelmadi.")]

            public string ConfirmPassword { get; set; }
        }
    }
}
