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
        private readonly DbSet<File> _files;

        public MaterialRepository(DbSet<Material> materials, DbSet<File> files) : base(materials)
        {
            _files = files;
        }

        public void CreateDeleteRequest(Material material)
        {
            throw new NotImplementedException();
        }

        public void CreateUpdateRequest(Material material)
        {
            throw new NotImplementedException();
        }

        public void RemoveFiles(ICollection<File> filesToRemove)
        {
            foreach (var fileToRemove in filesToRemove)
            {
                _files.Remove(fileToRemove);
            }
        }
    }
}
