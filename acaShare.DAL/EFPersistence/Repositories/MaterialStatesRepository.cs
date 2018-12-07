using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class MaterialStatesRepository : EFRepository<MaterialState>, IMaterialStateRepository
    {
        public MaterialStatesRepository(DbSet<MaterialState> dbSet) : base(dbSet)
        {
        }
    }
}
