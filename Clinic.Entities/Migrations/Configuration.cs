namespace Clinic.Entities.Migrations
{
    using Clinic.Entities.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Clinic.Entities.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Clinic.Entities.AppDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            //if (!roleManager.RoleExists("admin"))
            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var user = new ApplicationUser();
                user.Email = "admin@admin.pl";
                user.UserName = "admin@admin.pl";

                var passwordHasher = new PasswordHasher();
                user.PasswordHash = passwordHasher.HashPassword("admin12345");
                user.SecurityStamp = Guid.NewGuid().ToString();

                var adminRole = new IdentityRole("admin"); 
                context.Roles.Add(adminRole);

                var roleToUser = new IdentityUserRole();
                roleToUser.RoleId = adminRole.Id;
                roleToUser.UserId = user.Id;

                user.Roles.Add(roleToUser);
                context.Users.Add(user);
            }

        }
    }
}
