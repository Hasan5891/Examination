using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Examination.Data
{
    public static class DataSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
              
                UserManager<ApplicationUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<ApplicationRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                // Seed database code goes here

                // User Info
                //string userName = "SuperAdmin";
                string firstName = "Super";
                string lastName = "Admin";
                string email = "superadmin@admin.com";
                string password = "19460606";
                string role = "SuperAdmins";
                string role2 = "Admin";
                string role3 = "User";

                if (await _userManager.FindByNameAsync(email) == null)
                {
                    // Create SuperAdmins role if it doesn't exist
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new ApplicationRole(role));
                    }
                    if (await roleManager.FindByNameAsync(role2) == null)
                    {
                        await roleManager.CreateAsync(new ApplicationRole(role2));
                    }
                    if (await roleManager.FindByNameAsync(role3) == null)
                    {
                        await roleManager.CreateAsync(new ApplicationRole(role3));
                    }

                    // Create user account if it doesn't exist
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = "qwerty",
                        Email = email,
                        photo_url = "/img/tom.jpg",
                        DateRegistered = DateTime.UtcNow.ToString(),
                        FISH = firstName + " " + lastName
                                  
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, password);

                    // Assign role to the user
                    if (result.Succeeded)
                    {

                        await _userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }
    }
}
