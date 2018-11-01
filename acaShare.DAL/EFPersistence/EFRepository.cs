using acaShare.DAL.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acaShare.DAL.EFPersistence
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _dbSet;

        public EFRepository(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public TEntity FindById(int entityId)
        {
            return _dbSet.Find(entityId);
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
