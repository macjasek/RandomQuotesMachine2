using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace RandomQuotesMachine2.Data
{
    public class SampleData
    {
        public static async Task InitializeData(IServiceProvider services,
                ILoggerFactory loggerFactory,
                IConfiguration configuration)
        {
            var logger = loggerFactory.CreateLogger("Sample Data");

            using (var serviceScope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var env = serviceScope.ServiceProvider.GetService<IHostingEnvironment>();
                if (!env.IsDevelopment()) return;

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                //Create our roles
                var adminTask = roleManager.CreateAsync(
                    new IdentityRole { Name = "Admin" });
                Task.WaitAll(adminTask);

                logger.LogInformation("==> Added Admin roles");

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                //Create default user
                var user = new ApplicationUser
                {
                    Email = "admin@test.com",
                    UserName = "admin@test.com"
                };

                var adminPassword = configuration["adminPassword"];

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }

    

    
}
