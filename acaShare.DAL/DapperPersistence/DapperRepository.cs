using acaShare.DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;

namespace acaShare.DAL.DapperPersistence
{
    public class DapperRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static readonly string TEntityClassName = typeof(TEntity).Name;
        protected readonly IDbTransaction _transaction;
        private IDbConnection _con => _transaction.Connection;

        public DapperRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Add(TEntity entity)
        {
            string properties = string.Empty;
            int i = 0;
            foreach (var property in typeof(TEntity).GetProperties())
            {
                properties += property.GetValue(entity);

                if (i++ < properties.Length - 1)
                    properties += ", ";
            }

            _con.Execute($"INSERT INTO {TEntityClassName} VALUES ({properties})");
        }

        public void Delete(TEntity entity)
        {
            _con.Execute(
                $"DELETE FROM {TEntityClassName} WHERE {TEntityClassName}Id = {entity.GetType().GetProperty($"{TEntityClassName}Id").GetValue(entity)}");
        }

        public TEntity FindById(int entityId)
        {
            return _con.QuerySingle<TEntity>($"SELECT * FROM {TEntityClassName} WHERE {TEntityClassName}Id = {entityId}", transaction: _transaction);
        }

        public List<TEntity> GetAll()
        {
            return _con.Query<TEntity>($"SELECT * FROM {TEntityClassName}", transaction: _transaction).ToList();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
