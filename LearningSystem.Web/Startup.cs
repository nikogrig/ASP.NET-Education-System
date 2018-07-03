using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Infrastructure.DbExtension;
using AutoMapper;
using LearningSystem.Infrastructure.Identity;
using LearningSystem.Infrastructure.ServiceExtensions;
using Microsoft.AspNetCore.Mvc;
using static LearningSystem.Constants.AdminConstants;

namespace LearningSystem.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LearningSystemDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //Configure custom password requirerment
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<LearningSystemDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.

            // Add AutoMapper
            services.AddAutoMapper();

            // Add custom Service extension
            services.AddDomainServices();
            
            // Configure Urls - lower case
            services.AddRouting(routing => routing.LowercaseUrls = true);

            // Configure global Antiforgery
            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //Add custom extensions method for Db migrations and Identity roles
            //app.UseDatabaseMigration();
            app.UseDatabaseMigrationWithIdentities();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "profile",
                    template: "profile/users/{username}",
                    defaults: new { area = PROFILE_AREA, controller = "Users", action = "Index" });

                routes.MapRoute(
                    name: "modules",
                    template: "modules/courses/{courseId}/{name}",
                    defaults: new { area = MODULES_AREA, controller = "Courses", action = "Details" });

                routes.MapRoute(
                    name: "blog",
                    template: "blog/articles/{id}/{title}",
                    defaults: new { area = BLOG_AREA, controller = "Articles", action = "Details" });

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
