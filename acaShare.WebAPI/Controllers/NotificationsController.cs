using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace acaShare.WebAPI.Controllers
{
    [Authorize(Roles = Roles.MemberRole + ", " + Roles.AdministratorRole + ", " + Roles.MainModeratorRole + ", " + Roles.ModeratorRole)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public NotificationsController(INotificationService notificationService, IUserService userService, UserManager<IdentityUser> userManager)
        {
            _notificationService = notificationService;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<NotificationViewModel>> Get()
        {
            var appUserId = _userService.FindByIdentityUserId(_userManager.GetUserId(User))?.UserId;
            
            if (!appUserId.HasValue)
            {
                return Unauthorized();
            }

            var userNotifications = _notificationService.GetUserNotifications(appUserId.Value);

            return userNotifications.Select(n =>
                new NotificationViewModel
                {
                    MaterialId = n.MaterialId,
                    UserId = n.UserId,
                    Date = n.Date,
                    Content = n.Content
                }
            ).ToList();
        }
    }
}