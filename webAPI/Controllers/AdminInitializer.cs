using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using webAPI.Data;
using webAPI.Models;

namespace webAPI
{
    public static class AdminInitializer
    {
        public static async Task InitializeAdmin(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@12345";

            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    PhoneNumber = "1234567890",
                    FullName = "Admin User",
                    DateOfBirth = DateTime.Now.AddYears(-30),
                    Role = Roles.Admin
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                    context.Users.Add(adminUser);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
