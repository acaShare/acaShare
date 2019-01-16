using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.BLL.Models
{
    public interface IDeleteRequestsStatisticsData : IStatisticsData
    {
        int RequestsCount { get; set; }
        int CountOfActuallyDeleted { get; set; }
        IDictionary<string, int> ReasonsStatistics { get; set; }
    }
}
