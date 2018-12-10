using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface ISidebarService
    {
        ICollection<LastActivity> GetLastActivities();
        ICollection<Comment> GetComments(int materialId);
        ICollection<Material> GetFavoriteMaterials(string loggedUserId);
    }
}
