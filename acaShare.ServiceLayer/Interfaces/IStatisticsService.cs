using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface IStatisticsService
    {
        ICollection<Statistics> GetAvailableStatistics();
        ICollection<Statistics> GetAvailableDeleteRequestsStatistics();
        IDeleteRequestsStatisticsData GetDeleteRequestsGroupedByReason();
    }
}
