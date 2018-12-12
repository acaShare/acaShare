using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.BLL.Common
{
    public class Notifier
    {
        public Notification CreateNotificationForUser(NotificationType notificationType, IDictionary<string, string> data, User user)
        {
            string content = string.Empty;
            switch (notificationType)
            {
                case NotificationType.DELETE_REQUEST_APPROVED:
                    content = $"Twoja sugestia usunięcia materiału \"{data["MaterialName"]}\" została zaakceptowana.";
                    break;
                case NotificationType.DELETE_REQUEST_DECLINED:
                    content = $"Twoja sugestia usunięcia materiału \"{data["MaterialName"]}\" została odrzucona. Powód: {data["DeclineReason"]}.";
                    break;
                default:
                    content = "Wystąpił błąd w module powiadomień. Skontaktuj się z administratorem serwisu.";
                    break;
            }

            return new Notification(content, user);
        }
    }
}
