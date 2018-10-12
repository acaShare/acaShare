using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    public class ModeratorPanelService : IModeratorPanelService
    {
        private readonly IUnitOfWork _uow;

        public ModeratorPanelService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddUniversity(University university)
        {
            _uow.Universities.Add(university);
            _uow.SaveChanges();
        }
    }
}
