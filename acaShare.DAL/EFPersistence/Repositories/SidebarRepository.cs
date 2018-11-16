using acaShare.BLL.Models;
using acaShare.DAL.Configuration;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UserRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class SidebarRepository : ISidebarRepository
    {
        private readonly AcaShareDbContext _db;

        public SidebarRepository(AcaShareDbContext db)
        {
            _db = db;
        }
        
        public ICollection<Comment> GetComments(int materialId)
        {
            return _db.Comment.Where(c => c.MaterialId == materialId).ToList();
        }

        public ICollection<Favorites> GetFavorites()
        {
            return _db.Favorites.ToList();
        }

        public ICollection<LastActivity> GetLastActivities()
        {
            var comments = _db.Comment.ToList();
            var lastAddedMaterials = _db.Material.OrderByDescending(m => m.MaterialId).Take(7).ToList();

            var commentsActivities = comments.Select(c =>
                new LastActivity
                {
                    Username = c.User.Username,
                    Content = c.Content,
                    Date = c.CreatedDate,
                    Material = c.Material,
                    ActivityType = LastActivityType.COMMENT
                }
            );
            
            var materialsActivities = lastAddedMaterials.Select(m =>
                new LastActivity
                {
                    Username = m.Creator.Username,
                    Date = m.UploadDate,
                    Material = m,
                    ActivityType = LastActivityType.MATERIAL_ADD
                }    
            );

            var lastActivities = commentsActivities
                .Concat(materialsActivities)
                .OrderByDescending(a => a.Date)
                .ToList();

            if (lastActivities.Count > 7)
            {
                lastActivities = lastActivities.TakeLast(7).ToList();
            }

            return lastActivities;
        }
    }
}
