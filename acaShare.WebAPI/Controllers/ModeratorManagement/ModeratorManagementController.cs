using acaShare.BLL.Models;
using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.WebAPI.Controllers.ModeratorManagement
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

            var universitiesMainModeratorsWithIdentityId = new List<MainModeratorAppIdIdentityIdViewModel>();

            for (int i = 0; i < mainModerators.Length; i++)
            {
                var appUser = _userService.FindByIdentityUserId(mainModerators[i].Id);

                foreach (var item in universitiesMainModerators)
                {
                    if (appUser.UserId == item.UserId)
                    {
                        universitiesMainModeratorsWithIdentityId.Add(new MainModeratorAppIdIdentityIdViewModel
                        {
                            IdentityId = appUser.IdentityUserId,
                            UniversityId = item.UniversityId
                        });
                    }
                }
            }

            var model = new ModeratorManagementViewModel
            {
                Administrators = admins,
                MainModerators = mainModerators,
                Moderators = moderators,
                Members = members,
                Universities = universities,
                UniversitiesMainModeratorsWithIdentityId = universitiesMainModeratorsWithIdentityId
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

            if (userToPromote == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "użytkownik o podanym Id nie istnieje." });
            }

            var userRoles = (await _roleService.GetRolesAsync(userToPromote)).ToArray();
            await _roleService.RemoveFromRolesAsync(userToPromote, userRoles);
            await _roleService.AddToRoleAsync(userToPromote, Roles.MainModeratorRole);

            return RedirectToAction("Home");
        }

        public async Task<IActionResult> PromoteToModerator(string userId)
        {
            var userToPromote = await _roleService.FindByIdAsync(userId);

            if (userToPromote == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "użytkownik o podanym Id nie istnieje." });
            }

            var userRoles = (await _roleService.GetRolesAsync(userToPromote)).ToArray();
            await _roleService.RemoveFromRolesAsync(userToPromote, userRoles);
            await _roleService.AddToRoleAsync(userToPromote, Roles.ModeratorRole);

            return RedirectToAction("Home");
        }

        public async Task<IActionResult> DemoteToMember(string userId)
        {
            var userToDemote = await _roleService.FindByIdAsync(userId);

            if (userToDemote == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "użytkownik o podanym Id nie istnieje." });
            }

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

        [Authorize(Roles = Roles.AdministratorRole)]
        [HttpPost]
        public IActionResult AssignModeratorToUniversity(string userId, int UniversityId)
        {
            var appUser = _userService.FindByIdentityUserId(userId);

            if (appUser == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "użytkownik o podanym Id nie istnieje." });
            }

            if (UniversityId == 0)
            {
                return RedirectToAction("Home");
            }

            var newMainModInUniversity = new UniversityMainModerator
            {
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