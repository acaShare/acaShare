using acaShare.BLL.Models;
using acaShare.DAL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    /// <summary>
    /// For the thesis purposes we assume that every workspace is a University
    /// </summary>
    public class MainPanelService
    {
        private readonly IUnitOfWork _uow;

        public MainPanelService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<University> GetUniversities()
        {
            return _uow.Universities.GetAll();
        }
    }
}
