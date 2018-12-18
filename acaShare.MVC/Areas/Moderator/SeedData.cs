using acaShare.DAL.Configuration;
using acaShare.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator
{
    public static class SeedData
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            if(userManager.FindByEmailAsync("admin2@admin2.local").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin2@admin2.local",
                    Email = "admin2@admin2.local"
                };

                var _config = config.GetSection("AcaShareConfiguration").Get<AcaShareConfiguration>();


                IdentityResult result = userManager.CreateAsync(user, _config.AdminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.AdministratorRole).Wait();
                }
            }
        }
    }
}
