using Examination.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Infrastructure
{
    public class CustomUserValidator: UserValidator<ApplicationUser>

    {
        public CustomUserValidator():base()
              {

        }
        public override async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
          IdentityResult result1=await base.ValidateAsync(manager, user);
            var errors = new List<IdentityError>();

          //  await ValidateLogin(manager, user, errors);

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        private async Task ValidateLogin(UserManager<ApplicationUser> manager, ApplicationUser user, List<IdentityError> errors)
        {
             var name = await manager.GetUserNameAsync(user);
            var login = await Task.FromResult(user.FISH);
            
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(new IdentityError {

                    Code = nameof(ValidateLogin),
                    Description = "Bo'sh Login kiritish mumkin emas"
                });
                return;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(Describer.InvalidUserName(name));
                return;
            }


            var login1 = manager.NormalizeName(name);
            CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken cancellationToken = source.Token; 
            cancellationToken.ThrowIfCancellationRequested();
            
           var o= manager.Users.FirstOrDefaultAsync(u => u.FISH == login, cancellationToken).Result;

            if (o != null &&
                !string.Equals(await manager.GetUserIdAsync(o), await manager.GetUserIdAsync(user)))
            {
                errors.Add(new IdentityError
                {

                    Code = nameof(ValidateLogin),
                    Description = "Duplicate User Login"
                });
                return;
            }

            //var owner = await manager.FindByNameAsync(name);
            //if (owner != null && 
            //    !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            //{
            //    errors.Add(Describer.DuplicateUserName(name));
            //}

        }
    }
}
