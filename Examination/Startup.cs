using System;
using System.Linq;
using System.Text;
using Examination.Areas.Admin.Services;
using Examination.Data;
using Examination.Infrastructure;
using Examination.Service;
using Examination.Service.Catalog;
using Examination.Service.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Examination
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
          
            services.AddDbContext<ExaminationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ExaminationContext1>(options =>
         options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection1")));
           
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
               );

            services.AddIdentity<ApplicationUser, ApplicationRole>(opts =>
            {
                // Lockout settings
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                opts.Lockout.MaxFailedAccessAttempts = 3;
                opts.Lockout.AllowedForNewUsers = true;
                


            }).AddRoles<ApplicationRole>()           
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
          
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User settings.

                options.User.RequireUniqueEmail = false;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";





            });

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
      opt =>
      {
        //configure your other properties
        opt.LoginPath = "/Admin/Register/Login";
          opt.Cookie.HttpOnly = true;
          opt.ExpireTimeSpan = TimeSpan.FromMinutes(360);
          opt.LoginPath = "/Admin/Register/Login";
          opt.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
       
      });
            services.ConfigureApplicationCookie(options =>
            {

                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(360);
                options.LoginPath = "/Admin/Register/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
         
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<ITestRerulService, TestResultService>();

            //  services.AddScoped<ITestService, TestService>();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<SMSoptions>(Configuration);

            services.AddDistributedMemoryCache();

          
                services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(360);//You can set Time   
                options.IOTimeout = System.Threading.Timeout.InfiniteTimeSpan;
                
               
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("readonly", policy => policy.RequireRole("SuperAdmins", "Admin","User"));
                options.AddPolicy("writeonly", policy => policy.RequireRole("SuperAdmins", "Admin"));
                options.AddPolicy("Sysadmin", policy => policy.RequireRole("SuperAdmins"));
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
           
              

            app.UseRouting();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
               
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");



            });
           
           


          // DataSeed.Seed(app.ApplicationServices).Wait();
        }
    }
}
