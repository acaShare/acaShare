using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface ISidebarService
    {
        ICollection<Favorites> GetFavorites();
        ICollection<LastActivity> GetLastActivities();
        ICollection<Comment> GetComments(int materialId);
    }
}
