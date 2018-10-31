using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories.UserRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(DbSet<User> dbSet) : base(dbSet)
        {
        }
    }
}
