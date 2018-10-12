using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.DapperPersistence.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace acaShare.DAL.DapperPersistence
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        // Connectables
        private IDbConnection _con;
        private IDbTransaction _transaction;

        // Repositories
        private ILessonRepository _lessons;
        public ILessonRepository Lessons => _lessons ?? new LessonRepository(_transaction);

        public DapperUnitOfWork(IOptions<AcaShareConfiguration> options)
        {
            AcaShareConfiguration configuration = options.Value;
            var connStr = configuration.ConnectionString;

            _con = new SqlConnection(connStr);
            _con.Open();
            _transaction = _con.BeginTransaction();
        }

        public void SaveChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch(Exception e)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _con.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _lessons = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_con != null)
            {
                _con.Dispose();
                _con = null;
            }
        }
    }
}
