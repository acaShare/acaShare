using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    public class SidebarService : ISidebarService
    {
        private readonly IUnitOfWork _uow;

        public SidebarService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ICollection<Comment> GetComments(int materialId)
        {
            return _uow.SidebarRepository.GetComments(materialId);
        }

        public ICollection<LastActivity> GetLastActivities()
        {
            return _uow.SidebarRepository.GetLastActivities();
        }
    }
}
