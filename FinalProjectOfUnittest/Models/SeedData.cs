using FinalProjectOfUnittest.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectOfUnittest.Models
{
    public class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if(!context.Roles.Any())
            {
                List<string> roles = new List<string>()
                {
                    "Administrator","ProjectManager","Developer","Submitter"
                };

                foreach (string role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));

                }
            }
            //for seed
            if (!context.AppUser.Any())
            {
                AppUser user = new AppUser()
                {
                    Email = "Administrator@mitt.ca",
                    NormalizedEmail = "ADMINISTRATOR@MITT.CA",
                    UserName = "Administrator@mitt.ca",
                    NormalizedUserName = "ADMINISTRATOR@MITT.CA",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                AppUser user2 = new AppUser()
                {
                    Email = "Projectmanager@mitt.ca",
                    NormalizedEmail = "PROJECTMANAGER@MITT.CA",
                    UserName = "Projectmanager@mitt.ca",
                    NormalizedUserName = "PROJECTMANAGER@MITT.CA",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                AppUser user3 = new AppUser()
                {
                    Email = "Developer@mitt.ca",
                    NormalizedEmail = "DEVELOPER@MITT.CA",
                    UserName = "developer@mitt.ca",
                    NormalizedUserName = "DEVELOPER@MITT.CA",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                AppUser user4 = new AppUser()
                {
                    Email = "Submitter@mitt.ca",
                    NormalizedEmail = "SUBMITTER@MITT.CA",
                    UserName = "Submitter@mitt.ca",
                    NormalizedUserName = "SUBMITTER@MITT.CA",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var passwordHasher = new PasswordHasher<AppUser>();
                var hasedPassword = passwordHasher.HashPassword(user, "P@ssword1");
                user.PasswordHash = hasedPassword;
                await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, "Administrator");

                var passwordHasher2 = new PasswordHasher<AppUser>();
                var hasedPassword2 = passwordHasher2.HashPassword(user2, "P@ssword1");
                user2.PasswordHash = hasedPassword2;
                await userManager.CreateAsync(user2);
                await userManager.AddToRoleAsync(user2, "Projectmanager");

                var passwordHasher3 = new PasswordHasher<AppUser>();
                var hasedPassword3 = passwordHasher3.HashPassword(user3, "P@ssword1");
                user3.PasswordHash = hasedPassword3;
                await userManager.CreateAsync(user3);
                await userManager.AddToRoleAsync(user3, "Developer");

                var passwordHasher4 = new PasswordHasher<AppUser>();
                var hasedPassword4 = passwordHasher4.HashPassword(user4, "P@ssword1");
                user4.PasswordHash = hasedPassword4;
                await userManager.CreateAsync(user4);
                await userManager.AddToRoleAsync(user4, "Submitter");
            }
            context.SaveChanges();


        }
    }
}
