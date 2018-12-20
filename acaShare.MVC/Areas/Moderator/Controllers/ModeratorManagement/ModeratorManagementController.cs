using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace acaShare.MVC.Areas.Moderator.Controllers.ModeratorManagement
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole)]
    [Area("Moderator")]
    public class ModeratorManagementController : Controller
    {
        private readonly IRolesManagementService _service;

        public ModeratorManagementController(IRolesManagementService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Home()
        {
            var admins = (await _service
                .GetUsersInRoleAsync(Roles.AdministratorRole))
                .ToArray();

            var mainModerators = (await _service
                .GetUsersInRoleAsync(Roles.MainModeratorRole))
                .ToArray();

            var moderators = (await _service
                .GetUsersInRoleAsync(Roles.ModeratorRole))
                .ToArray();

            var members = (await _service
                .GetUsersInRoleAsync(Roles.MemberRole))
                .ToArray();

            var model = new ModeratorManagementViewModel
            {
                Administrators = admins,
                MainModerators = mainModerators,
                Moderators = moderators,
                Members = members
            };

            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "ModeratorManagementController",
                    Action = "Home",
                    Title = "Zarządzaj Moderatorami"
                }
            };
            return View(model);
        }

        [Authorize(Roles = Roles.AdministratorRole)]
        public async Task<IActionResult> PromoteToMainModerator(string userId)
        {
            var userToPromote = await _service.FindByIdAsync(userId);

            var userRoles = (await _service.GetRolesAsync(userToPromote)).ToArray();
            await _service.RemoveFromRolesAsync(userToPromote, userRoles);
            await _service.AddToRoleAsync(userToPromote, Roles.MainModeratorRole);

            return RedirectToAction("Home");
        }

        public async Task<IActionResult> PromoteToModerator(string userId)
        {
            var userToPromote = await _service.FindByIdAsync(userId);

            var userRoles = (await _service.GetRolesAsync(userToPromote)).ToArray();
            await _service.RemoveFromRolesAsync(userToPromote, userRoles);
            await _service.AddToRoleAsync(userToPromote, Roles.ModeratorRole);

            return RedirectToAction("Home");
        }

        public async Task<IActionResult> DemoteToMember(string userId)
        {
            var userToDemote = await _service.FindByIdAsync(userId);

            var userRoles = (await _service.GetRolesAsync(userToDemote)).ToArray();
            await _service.RemoveFromRolesAsync(userToDemote, userRoles);
            await _service.AddToRoleAsync(userToDemote, Roles.MemberRole);

            return RedirectToAction("Home");
        }
    }
}