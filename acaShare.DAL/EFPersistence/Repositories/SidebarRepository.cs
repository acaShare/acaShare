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
        private readonly DbSet<Comment> _comments;
        private readonly DbSet<Material> _materials;
        private readonly DbSet<Favorites> _favorites;

        public SidebarRepository(DbSet<Comment> comments, DbSet<Material> materials, DbSet<Favorites> favorites)
        {
            _comments = comments;
            _materials = materials;
            _favorites = favorites;
        }
        
        public ICollection<Comment> GetComments(int materialId)
        {
            return _comments.Where(c => c.MaterialId == materialId).ToList();
        }

        public ICollection<LastActivity> GetLastActivities()
        {
            var commentsActivities = _comments
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

            var materialsActivities = _materials
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
            return _favorites
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
