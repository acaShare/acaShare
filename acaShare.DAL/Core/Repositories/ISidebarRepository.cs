using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories
{
    public interface ISidebarRepository
    {
        ICollection<Favorites> GetFavorites();
        ICollection<LastActivity> GetLastActivities();
        ICollection<Comment> GetComments(int materialId);
    }
}
