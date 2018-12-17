using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.BLL.Common
{
    public static class Notifier
    {
        public static Notification CreateNotificationForUser(NotificationType notificationType, IDictionary<string, string> data, User user)
        {
            string content = string.Empty;
            switch (notificationType)
            {
                case NotificationType.DELETE_REQUEST_APPROVED:
                    content = data.ContainsKey("DeleteReason") ?
                        $"Sugestia usunięcia stworzonego przez Ciebie materiału \"{data["MaterialName"]}\" została zaakceptowana. Powód usunięcia: {data["DeleteReason"]}."
                        :
                        $"Twoja sugestia usunięcia materiału \"{data["MaterialName"]}\" została zaakceptowana.";
                    break;

                case NotificationType.DELETE_REQUEST_DECLINED:
                    string reason = data.TryGetValue("DeclineReason", out string r) ? r : "Nie podano";
                    content = $"Twoja sugestia usunięcia materiału \"{data["MaterialName"]}\" została odrzucona. Powód: {reason}.";
                    break;

                case NotificationType.UPDATE_REQUEST_APPROVED:
                    content = data.ContainsKey("IsCreator") ?
                        $"Sugestia edycji Twojego materiału \"{data["MaterialName"]}\" została zaakceptowana. Podsumowanie edycji: {data["EditSummary"]}."
                        :
                        $"Twoja sugestia edycji materiału \"{data["MaterialName"]}\" została zaakceptowana. Podsumowanie edycji: {data["EditSummary"]}.";
                    break;

                case NotificationType.UPDATE_REQUEST_DECLINED:
                    string reason2 = data.TryGetValue("DeclineReason", out string rr) ? rr : "Nie podano";
                    content = $"Twoja sugestia edycji materiału \"{data["MaterialName"]}\" została odrzucona. Powód: {reason2}.";
                    break;

                default:
                    content = "Wystąpił błąd w module powiadomień. Skontaktuj się z administratorem serwisu.";
                    break;
            }

            return new Notification(content, user);
        }
    }
}
