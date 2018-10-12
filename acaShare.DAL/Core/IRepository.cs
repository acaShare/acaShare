using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity FindById(int entityId);
        void Add(TEntity entity);
        void Delete(TEntity entity);
    }
}
