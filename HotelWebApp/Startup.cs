using HotelWebApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotelWebApp.Models;

namespace HotelWebApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddControllersWithViews();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "IsReceptionist",
                    policyBuilder => policyBuilder
                        .RequireClaim("Receptionist"));

                options.AddPolicy(
                    "IsWaiter",
                    policyBuilder => policyBuilder
                        .RequireClaim("Waiter"));

                options.AddPolicy(
                    "IsChef",
                    policyBuilder => policyBuilder
                        .RequireClaim("Chef"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }

            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            //SeedUsers(userManager, context); //Seeding users
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            ApplicationDbContext _context = context;
            const bool emailConfirmed = true;

            //=================== Creating Receptionist ==========================

            const string receptionistEmail = "receptionist@receptionist.com";
            const string receptionistPassword = "Sommer25!";
            const string receptionistCell = "20212223";
            const string receptionistName = "Peter Fuglsang";


            if (!_context.Receptionists.Any(e => e.UserName == receptionistEmail))
            {
                var user = new Receptionist();
                user.UserName = receptionistEmail;
                user.Name = receptionistName;
                user.Email = receptionistEmail;
                user.EmailConfirmed = emailConfirmed;
                user.PhoneNumber = receptionistCell;

                IdentityResult result = userManager.CreateAsync(user, receptionistPassword).Result;

                if (result.Succeeded) //Add claim to user
                {
                    userManager.AddClaimAsync(user, new Claim("Receptionist", "IsReceptionist")).Wait();
                }

                //_context.Receptionists.Add(user);
                //_context.SaveChanges();
            }

            
        }
    }
}
