using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DbSet<DeleteRequest> _deleteRequests;

        public StatisticsRepository(DbSet<DeleteRequest> editRequests)
        {
            _deleteRequests = editRequests;
        }

        public ICollection<DeleteRequest> GetDeleteRequests()
        {
            return _deleteRequests.Include(dr => dr.DeleteReason).ToList();
        }
    }
}
