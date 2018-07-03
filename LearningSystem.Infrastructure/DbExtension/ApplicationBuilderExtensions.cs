using LearningSystem.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Infrastructure.DbExtension
{
    public static class ApplicationBuilderExtensions
    {
        //Not using now, because we have a same method with identity
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app
                                         .ApplicationServices
                                         .GetRequiredService<IServiceScopeFactory>()
                                         .CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<LearningSystemDbContext>()
                    .Database
                    .Migrate();
            }

            return app;
        }
    }
}