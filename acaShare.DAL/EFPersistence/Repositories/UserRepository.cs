using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories.UserRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(DbSet<User> dbSet) : base(dbSet)
        {
        }

        public User FindByIdentityUserId(string identityUserId)
        {
            return _dbSet.First(u => u.IdentityUserId == identityUserId);
        }
        
        public bool IsMaterialFavorite(string identityUserId, int materialId)
        {
            return _dbSet
                .Where(u => u.IdentityUserId == identityUserId)
                .SelectMany(u => u.Favorites)
                .Any(f => f.MaterialId == materialId);
        }
    }
}
