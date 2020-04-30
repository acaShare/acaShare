using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface IStatisticsService
    {
        IReadOnlyCollection<Statistics> GetAvailableStatistics();
        IReadOnlyCollection<Statistics> GetAvailableDeleteRequestsStatistics();
        IDeleteRequestsStatisticsData GetDeleteRequestsGroupedByReason();
    }
}
