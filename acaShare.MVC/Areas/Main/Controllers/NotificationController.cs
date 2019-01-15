using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Main.Models;
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

            var notifications = userNotifications.Select(n =>
                new NotificationViewModel
                {
                    MaterialId = n.MaterialId,
                    UserId = n.UserId,
                    Date = n.Date,
                    Content = n.Content
                }
            );
            
            return Json(notifications);
        }
    }
}