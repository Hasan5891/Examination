using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Data
{
    public class ApplicationUser : IdentityUser
    {

       
        public byte[] AvatarImage { get; set; }
        public string FISH { get; set; }
        public string photo_url { get; set; }
        public string DateRegistered { get; set; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
       [DataType("decimal(8 ,0)")]
       [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Price { get; set; }
       
    }
}
