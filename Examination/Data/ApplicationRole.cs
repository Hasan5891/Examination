using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Data
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole(string name):base(name)
        {
                
        }
        public ApplicationRole() : base()
        {

        }

        public string Description { get; set; }
    }

}
