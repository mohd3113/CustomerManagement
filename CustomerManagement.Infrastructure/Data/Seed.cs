using Microsoft.AspNetCore.Identity;

namespace CustomerManagement.Infrastructure.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, CMDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole{Name = "Customer"},
                    new IdentityRole{Name = "Admin"}
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                var adminUser = new IdentityUser
                {
                    UserName = "Admin",
                    Email = "TestDanskeBank@hotmail.com",
                    PhoneNumber = "0037000000000"
                };

                var result = userManager.CreateAsync(adminUser, "TestTest1@").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(admin, new[] { "Admin" }).Wait();
                }
            }
        }
    }
}