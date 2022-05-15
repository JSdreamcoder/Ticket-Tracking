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
                    Email = "Developer001@mitt.ca",
                    NormalizedEmail = "DEVELOPER001@MITT.CA",
                    UserName = "developer001@mitt.ca",
                    NormalizedUserName = "DEVELOPER001@MITT.CA",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                AppUser user4 = new AppUser()
                {
                    Email = "Jaewon@gmail.com",
                    NormalizedEmail = "JAEWON@GMAIL.COM",
                    UserName = "Jaewon@gmail.com",
                    NormalizedUserName = "JAEWON@GMAIL.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                AppUser user5 = new AppUser()
                {
                    Email = "bob@gmail.com",
                    NormalizedEmail = "BOB@GMAIL.COM",
                    UserName = "bob@gmail.com",
                    NormalizedUserName = "BOB@GMAIL.COM",
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

                var passwordHasher5 = new PasswordHasher<AppUser>();
                var hasedPassword5 = passwordHasher5.HashPassword(user5, "P@ssword1");
                user4.PasswordHash = hasedPassword5;
                await userManager.CreateAsync(user5);
                await userManager.AddToRoleAsync(user5, "Submitter");
            }
            // Seed project
            if (!context.Project.Any())
            {
                Project project1 = new Project();
                project1.Name = "QnAPage";

                Project project2 = new Project();
                project1.Name = "TaskManagement";
            }

            if (!context.Ticket.Any())
            {
                Ticket ticket1 = new Ticket();
                ticket1.Title = "Bug Report Of Answering";
                ticket1.Description = "I found a Bug When I make new Answer with emty title. After clicking Summit, I got Bad connection error";
                ticket1.Created = DateTime.Now;
                ticket1.ProjectId = 1;
                ticket1.TicketType = TicketTypes.BugReport;
                ticket1.TicketPriority =  TicketPriorities.High;
                ticket1.TicketStatus = TicketStatus.Assigned;
                ticket1.OwnerUserId = context.AppUser.First(u => u.UserName == "Jaewon@gmail.com").Id;
                ticket1.AssignedToUserId = context.AppUser.First(u => u.UserName == "developer001@mitt.ca").Id;


            }
                context.SaveChanges();


        }
    }
}
