using acaShare.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await EnsureRolesAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            await EnsureTestAdminAsync(userManager);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var administratorAlreadyExists = await roleManager.RoleExistsAsync(Roles.AdministratorRole);
            var mainModeratorAlreadyExists = await roleManager.RoleExistsAsync(Roles.MainModeratorRole);
            var moderatorAlreadyExists = await roleManager.RoleExistsAsync(Roles.ModeratorRole);
            var memberAlreadyExists = await roleManager.RoleExistsAsync(Roles.MemberRole);

            if (!administratorAlreadyExists)
                await roleManager.CreateAsync(new IdentityRole(Roles.AdministratorRole));

            if(!mainModeratorAlreadyExists)
                await roleManager.CreateAsync(new IdentityRole(Roles.MainModeratorRole));

            if(!moderatorAlreadyExists)
                await roleManager.CreateAsync(new IdentityRole(Roles.ModeratorRole));

            if (!memberAlreadyExists)
                await roleManager.CreateAsync(new IdentityRole(Roles.MemberRole));
        }

        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users.Where(x => x.UserName == "admin@admin.local").SingleOrDefaultAsync();

            if (testAdmin != null) return;

            testAdmin = new IdentityUser
            {
                UserName = "admin@admin.local",
                Email = "admin@admin.local"
            };

            await userManager.CreateAsync(testAdmin, "NotSecure123!");

            await userManager.AddToRoleAsync(testAdmin, Roles.AdministratorRole);
        }


    }
}
