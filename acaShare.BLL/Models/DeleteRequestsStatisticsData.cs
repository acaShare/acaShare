using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.BLL.Models
{
    public class DeleteRequestsStatisticsData : IDeleteRequestsStatisticsData
    {
        public int RequestsCount { get; set; }
        public int CountOfActuallyDeleted { get; set; }
        public IDictionary<string, int> ReasonsStatistics { get; set; }
    }
}
