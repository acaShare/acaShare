using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    public class MainPanelService : IMainPanelService
    {
        private readonly IUnitOfWork _uow;

        public MainPanelService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<University> GetAvailableUniversities()
        {
            return _uow.Universities.GetAll();
        }
    }
}
