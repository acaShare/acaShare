using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Main.Controllers
{
    [Area("Main")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public NotificationController(INotificationService notificationService, IUserService userService)
        {
            _notificationService = notificationService;
            _userService = userService;
        }

        public IActionResult NotificationData()
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUserId = _userService.FindByIdentityUserId(identityUserId).UserId;
            var userNotifications = _notificationService.GetUserNotifications(appUserId);

            return Json(userNotifications);
        }
    }
}