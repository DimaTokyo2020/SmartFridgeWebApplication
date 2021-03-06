using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using SmartFridge.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartFridge.Models;
using SmartFridgeWebApllication.Service;


using Microsoft.AspNetCore.Identity.UI.Services;


namespace SmartFridge
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
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DevicesContext>(options =>
                options.UseSqlServer(connection));
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            //automatic add migration!!!!
            services.BuildServiceProvider().GetService<DevicesContext>().Database.Migrate();


            //send confirmation email by SendGrid
            services.AddTransient<IEmailSender, EmailSenderService>(options =>
            {
                var username = Configuration["SendGrid:Username"];
                var apiKey = Configuration["SendGrid:ApiKey"];

                return new EmailSenderService(username, apiKey);
            });

            
            services.AddAuthentication()
                .AddGoogle(option =>
                {
                    option.ClientId = Configuration["GoogleLoginClient:ClientId"];
                    option.ClientSecret = Configuration["GoogleLogInClient:ClientSecret"];

                });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Entry}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}




