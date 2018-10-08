using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.Core.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace acaShare.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _con;

        public UnitOfWork(IOptions<AcaShareConfiguration> options)
        {
            AcaShareConfiguration configuration = options.Value;
            var connStr = configuration.ConnectionString;
            _con = new SqlConnection(connStr);
            _con.Open();
        }

        public ILessonRepository Lessons { get; }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
