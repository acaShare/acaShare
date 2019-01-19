﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.BLL.Models;
using acaShare.MVC.Areas.Moderator.Models;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace acaShare.MVC.Areas.Moderator.Controllers.ModeratorManagement
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole)]
    [Area("Moderator")]
    public class ModeratorManagementController : Controller
    {
        private readonly IRolesManagementService _roleService;
        private readonly IUniversityTreeTraversalService _universityService;
        private readonly IMainModeratorService _mainModeratorService;
        private readonly IUserService _userService;

        public ModeratorManagementController(IRolesManagementService roleService, IUniversityTreeTraversalService universityService, IMainModeratorService mainModeratorService, IUserService userService)
        {
            _roleService = roleService;
            _universityService = universityService;
            _mainModeratorService = mainModeratorService;
            _userService = userService;
        }

        public async Task<IActionResult> Home()
        {
            var admins = (await _roleService
                .GetUsersInRoleAsync(Roles.AdministratorRole))
                .ToArray();

            var mainModerators = (await _roleService
                .GetUsersInRoleAsync(Roles.MainModeratorRole))
                .ToArray();

            var moderators = (await _roleService
                .GetUsersInRoleAsync(Roles.ModeratorRole))
                .ToArray();

            var members = (await _roleService
                .GetUsersInRoleAsync(Roles.MemberRole))
                .ToArray();

            var universities = _universityService
                .GetUniversities().ToList();

            var universitiesMainModerators = _mainModeratorService
                .GetAllUniversitiesMainModerators();

            var model = new ModeratorManagementViewModel
            {
                Administrators = admins,
                MainModerators = mainModerators,
                Moderators = moderators,
                Members = members,
                Universities = universities,
                UniversitiesMainModerators = universitiesMainModerators
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
            var userToPromote = await _roleService.FindByIdAsync(userId);

            var userRoles = (await _roleService.GetRolesAsync(userToPromote)).ToArray();
            await _roleService.RemoveFromRolesAsync(userToPromote, userRoles);
            await _roleService.AddToRoleAsync(userToPromote, Roles.MainModeratorRole);

            return RedirectToAction("Home");
        }

        public async Task<IActionResult> PromoteToModerator(string userId)
        {
            var userToPromote = await _roleService.FindByIdAsync(userId);

            var userRoles = (await _roleService.GetRolesAsync(userToPromote)).ToArray();
            await _roleService.RemoveFromRolesAsync(userToPromote, userRoles);
            await _roleService.AddToRoleAsync(userToPromote, Roles.ModeratorRole);

            return RedirectToAction("Home");
        }

        public async Task<IActionResult> DemoteToMember(string userId)
        {
            var userToDemote = await _roleService.FindByIdAsync(userId);

            var userRoles = (await _roleService.GetRolesAsync(userToDemote)).ToArray();
            await _roleService.RemoveFromRolesAsync(userToDemote, userRoles);
            await _roleService.AddToRoleAsync(userToDemote, Roles.MemberRole);

            var appUser = _userService.FindByIdentityUserId(userId);
            var moderatorInUniversity = _mainModeratorService.GetUniversityMainModerator(appUser.UserId);

            if (moderatorInUniversity != null)
            {
                _mainModeratorService.UnassignMainModeratorFromUniversity(moderatorInUniversity);
            }

            return RedirectToAction("Home");
        }

        [HttpPost]
        public IActionResult AssignModeratorToUniversity(string userId, int UniversityId)
        {
            var appUser = _userService.FindByIdentityUserId(userId);

            var newMainModInUniversity = new UniversityMainModerator {
                UserId = appUser.UserId,
                UniversityId = UniversityId
            };

            if (_mainModeratorService.UniversityMainModeratorExists(appUser.UserId))
            {
                _mainModeratorService.EditMainModeratorAssignement(newMainModInUniversity);
            }
            else
            {
                _mainModeratorService.AssignMainModeratorToUniversity(newMainModInUniversity);
            }

            return RedirectToAction("Home");
        }
    }
}