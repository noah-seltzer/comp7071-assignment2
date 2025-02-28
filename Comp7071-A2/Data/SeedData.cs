using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Comp7071_A2.Data
{
    public static class SeedData
    {
        public static async Task SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure the database is created and migrated
            //await context.Database.MigrateAsync();

            // Create roles if they don't exist
            string[] roleNames = { "HousingAdmin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create default users
            await CreateUser(userManager, "admin@housing.com", "Admin123!", "HousingAdmin");
            await CreateUser(userManager, "user1@housing.com", "User123!", "User");
            await CreateUser(userManager, "user2@housing.com", "User123!", "User");
            await CreateUser(userManager, "user3@housing.com", "User123!", "User");
        }

        private static async Task CreateUser(UserManager<IdentityUser> userManager, string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    UserName = email,
                    Email = email
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
