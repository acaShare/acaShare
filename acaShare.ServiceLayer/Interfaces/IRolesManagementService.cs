using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface IRolesManagementService
    {
        Task<IList<IdentityUser>> GetUsersInRoleAsync(string administratorRole);
        Task<IdentityUser> FindByIdAsync(string userId);
        Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role);
        Task<IdentityResult> RemoveFromRoleAsync(IdentityUser user, string role);
        Task<IList<string>> GetRolesAsync(IdentityUser user);
        Task<IdentityResult> RemoveFromRolesAsync(IdentityUser user, IEnumerable<string> roles);
    }
}
