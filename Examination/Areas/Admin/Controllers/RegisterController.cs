using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Examination.Areas.Admin.Models;
using Examination.Areas.Admin.Services;
using Examination.Data;
using Examination.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;




namespace Examination.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RegisterController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ISmsSender _emailSender;
        private readonly IAsyncRepository<User> _UserRepository;

        public RegisterController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            ISmsSender emailSender, IAsyncRepository<User> UserRepository)
        {
            _UserRepository = UserRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        

        }
        
        public IActionResult Index(string returnUrl = null)
        {
          
            returnUrl = returnUrl ?? Url.Content("~/");
            RegisterModel model = new RegisterModel { ReturnUrl = returnUrl };
            return View(model);
        }

       

        [HttpPost]
       
        public async Task<IActionResult> Create(RegisterModel createVm)
        {

            

            if (ModelState.IsValid)
            {
                       var user = new ApplicationUser { UserName = createVm.UserName, FISH = createVm.FISH, DateRegistered = DateTime.UtcNow.ToString() };

                    var result = await _userManager.CreateAsync(user, createVm.Password);
                      if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code },
                            protocol: Request.Scheme);

                    await _emailSender.SendSmsAsync("998946180585", "123123");
                        var std = new User()
                        {
                            ID = user.Id,
                            Nomi = user.FISH


                        };
                        await _UserRepository.AddAsync(std);
                        //  context.Students.Add(std);

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return Redirect(createVm.ReturnUrl);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
              

            }

            // If we got this far, something failed, redisplay form

            return View(nameof(Index));
        }
        public  IActionResult Login(string returnUrl = null)
        {
            
            returnUrl = returnUrl ?? Url.Content("~/");
            LoginModel model = new LoginModel() { ReturnUrl = returnUrl};
            return View(model);
        }
        [HttpPost,ActionName("Login")]
        public async Task<IActionResult> LoginCreate(LoginModel model)
        {
           

            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password,true, lockoutOnFailure: true);


                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(model.ReturnUrl);
                }
                if (result.IsLockedOut)
                {

                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Parol yoki login no'to'g'ri ");
                    return View(model);
                }


            }

            ModelState.AddModelError(string.Empty, "Hatolik yuz berdi ");

            return View(model);

        }
       

    }
}