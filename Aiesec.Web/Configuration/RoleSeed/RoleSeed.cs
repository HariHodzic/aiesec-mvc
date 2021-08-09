using System;
using System.Threading.Tasks;
using Aiesec.Data.Model.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiesec.Web.Configuration.RoleSeed
{
    public static class RoleSeed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleNames = SystemRoles.Roles;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist) await roleManager.CreateAsync(new ApplicationRole(roleName));
            }

            var user = await userManager.FindByNameAsync("admin");

            if (user == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin", Email = "rsfitmostar@outlook.com", Gender = Gender.Other
                };
                const string adminPassword = "Password123";
                var createAdmin = await userManager.CreateAsync(admin, adminPassword);
                if (createAdmin.Succeeded) await userManager.AddToRoleAsync(admin, "Super Admin");
            }
        }
    }
}