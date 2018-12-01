using acaShare.BLL.Models;
using acaShare.DAL.Configuration;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UserRelated;
using Microsoft.EntityFrameworkCore;
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

        public ICollection<LastActivity> GetLastActivities()
        {
            var commentsActivities = _db.Comment
                .Include(c => c.User)
                .Include(c => c.Material)
                .OrderByDescending(c => c.CommentId)
                .Take(10)
                .Select(c =>
                    new LastActivity
                    {
                        Username = c.User.Username,
                        Content = c.Content,
                        Date = c.CreatedDate,
                        Material = c.Material,
                        ActivityType = LastActivityType.COMMENT
                    })
                .ToList();

            var materialsActivities = _db.Material
                .Include(m => m.Creator)
                .OrderByDescending(m => m.MaterialId)
                .Take(10)
                .Select(m =>
                    new LastActivity
                    {
                        Username = m.Creator.Username,
                        Date = m.UploadDate,
                        Material = m,
                        ActivityType = LastActivityType.MATERIAL_ADD
                    })
                .ToList();

            var lastActivities = commentsActivities
                .Concat(materialsActivities)
                .OrderByDescending(a => a.Date)
                .ToList();

            if (lastActivities.Count > 10)
            {
                lastActivities = lastActivities.Take(10).ToList();
            }

            return lastActivities;
        }

        public ICollection<Material> GetFavoriteMaterials(string loggedUserId)
        {
            return _db.Favorites
                .Where(f => f.User.IdentityUserId == loggedUserId)
                .Select(f => f.Material)
                .Include(m => m.Lesson)
                .Include(m => m.Lesson.Semester)
                .Include(m => m.Lesson.SubjectDepartment)
                .Include(m => m.Lesson.SubjectDepartment.Subject)
                .Include(m => m.Lesson.SubjectDepartment.Department)
                .Include(m => m.Lesson.SubjectDepartment.Department.University)
                .ToList();
        }
    }
}
