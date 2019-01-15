using acaShare.BLL.Models;
using acaShare.DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public class StatisticsService : IStatisticsService
    {
        public readonly IUnitOfWork _uow;

        public StatisticsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ICollection<Statistics> GetAvailableStatistics()
        {
            return new List<Statistics> { new Statistics { Name = "Statystyki sugestii usunięcia" } };
        }

        public IDeleteRequestsStatisticsData GetDeleteRequestsStatistics()
        {
            var deleteRequests = _uow.StatisticsRepository.GetDeleteRequests();

            var statisticsData = new DeleteRequestsStatisticsData
            {
                RequestsCount = deleteRequests.Count(),
                CountOfActuallyDeleted = deleteRequests.Where(dr => dr.RequestState == RequestState.APPROVED).Count(),
                ReasonsStatistics = 
                    deleteRequests.GroupBy(dr => dr.DeleteReason.Reason)
                        .Select(group =>
                            new {
                                Name = group.Key,
                                Count = group.Count()
                            })
                        .ToDictionary(o => o.Name, o => o.Count)
                        //jakie przyczyny w danyhm miesiacu, ile sugestii w miesiacu, ile zaakceptowanych w miesiacu, ile odrzuconych w miesiacu
            };
            
            return statisticsData;
        }
    }
}
