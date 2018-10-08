using acaShare.DAL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Persistence
{
    public class DapperRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity FindById(int entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
