using Examination.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.Admin.Models
{
    public class RegisterModel
    {
        
        
            [Required(ErrorMessage = "Login kiritish talab etiladi")]
            [Display(Name = "Login")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "FISH kiritish talab etiladi")]
            [Display(Name = "FISH")]
            public string FISH { get; set; }


            [Required(ErrorMessage = "Parol kiritish talab etiladi")]
            [StringLength(100, ErrorMessage = " {0} maksimal {1} va minimal {2} simvol kiriting", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Parol")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Parolni tasdiqlash")]
            [Compare("Password", ErrorMessage = "Parol va parol tasdiqlash mos kelmadi.")]
            public string ConfirmPassword { get; set; }
            public string ReturnUrl { get; set; }
        

    }
    public class LoginModel
    {
        
      
        [Required(ErrorMessage = "Login talab etiladi")]
        public string Login { get; set; }

       
        [Required(ErrorMessage = "Parol kiritish talab etiladi")]
        [Display(Name = "Parol")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public string BotName { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }




    }

    

}
