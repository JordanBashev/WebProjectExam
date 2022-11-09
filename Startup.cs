using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;
using WebProjectExam.Services.OrderSevices;
using WebProjectExam.Services.ShoeServices;
using WebProjectExam.Services.UserServices;

namespace WebProjectExam
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
            //Connection to Database
            services.AddDbContext<ShoeStoreDbContext>(options
                    => options.UseSqlServer(Configuration["connectionString:DefaultConnection"]));
            
            //Add identity
            services.AddIdentity<User, IdentityRole>(options => options.Password = new PasswordOptions
            {
                RequireDigit = false,
                RequiredLength = 1,
                RequireLowercase = false,
                RequireUppercase = false,
                RequiredUniqueChars = 0,
                RequireNonAlphanumeric = false
            })
    .AddEntityFrameworkStores<ShoeStoreDbContext>()
    .AddDefaultTokenProviders();



            services.AddMvc();
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IOrderServices ,OrderServices>();
            services.AddScoped<IShoeServices, ShoeServices>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
