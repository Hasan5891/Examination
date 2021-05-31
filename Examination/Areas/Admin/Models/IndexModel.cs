using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Areas.Admin.Models
{
    public class IndexModel

    {
        [Display(Name = "Login")]
        public string Username { get; set; }


        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

       
    }

    public class InputModel
    {

        [Display(Name = "FISH")]
        public string FISH { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public IFormFile AvatarImage { get; set; }
        [Phone]
        [Display(Name = "Telefon raqami")]
        public string PhoneNumber { get; set; }
    }

    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string ChangePassword => "Password";

        public static string ExternalLogins => "ExternalLogins";

        public static string PersonalData => "PersonalData";
        public static string Reult => "MyResult";


        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);
        public static string ResultNavClass(ViewContext viewContext) => PageNavClass(viewContext, Reult);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        private static string PageNavClass(ViewContext viewContext, string action)
        {
            var activePage = viewContext.RouteData.Values["action"].ToString()
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, action, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
