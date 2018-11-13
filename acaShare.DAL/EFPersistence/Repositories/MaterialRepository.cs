using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public sealed class MaterialRepository : EFRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(DbSet<Material> dbSet) : base(dbSet)
        {
        }

        public void CreateDeleteRequest(Material material)
        {
            throw new NotImplementedException();
        }

        public void CreateUpdateRequest(Material material)
        {
            throw new NotImplementedException();
        }
    }
}
