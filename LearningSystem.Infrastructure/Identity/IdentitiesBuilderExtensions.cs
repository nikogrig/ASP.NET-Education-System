using LearningSystem.Constants;
using LearningSystem.Data;
using LearningSystem.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Infrastructure.Identity
{
    public static class IdentitiesBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigrationWithIdentities(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope
                    .ServiceProvider
                    .GetService<LearningSystemDbContext>();

                context.Database.Migrate();

                var userManager = serviceScope
                     .ServiceProvider
                     .GetService<UserManager<User>>();

                var roleManager = serviceScope
                    .ServiceProvider
                    .GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminRoleName = IdentitiesConstants.ADMINISTRATOR_ROLE;

                        var roles = new[]
                        {
                            adminRoleName,
                            IdentitiesConstants.AUTHOR_ROLE,
                            IdentitiesConstants.TRAINER_ROLE
                        };

                        foreach (var role in roles)
                        {
                            bool roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        var adminEmail = "admin@myweb.com";

                        var adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = adminEmail,
                                UserName = adminRoleName,
                                Name = adminRoleName,
                                Birthdate = DateTime.UtcNow,
                            };

                            await userManager.CreateAsync(adminUser, "admin123");

                            await userManager.AddToRoleAsync(adminUser, adminRoleName);
                        }
                    })
                .Wait();
            }

            return app;
        }
    }
}
