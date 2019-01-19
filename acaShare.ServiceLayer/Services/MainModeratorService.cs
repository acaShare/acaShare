using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    public class MainModeratorService : IMainModeratorService
    {
        private readonly IUnitOfWork _uow;

        public MainModeratorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AssignMainModeratorToUniversity(UniversityMainModerator universityMainModerator)
        {
            _uow.MainModeratorRepository.AssignMainModeratorToUniversity(universityMainModerator);
            _uow.SaveChanges();
        }

        public void UnassignMainModeratorFromUniversity(UniversityMainModerator universityMainModerator)
        {
            _uow.MainModeratorRepository.UnassignMainModeratorFromUniversity(universityMainModerator);
            _uow.SaveChanges();
        }

        public UniversityMainModerator GetUniversityMainModerator(int userId)
        {
            return _uow.MainModeratorRepository.GetUniversityMainModerator(userId);
        }

        public void EditMainModeratorAssignement(UniversityMainModerator universityMainModerator)
        {
            _uow.MainModeratorRepository.EditMainModeratorAssignement(universityMainModerator);
            _uow.SaveChanges();
        }

        public bool UniversityMainModeratorExists(int userId)
        {
            return _uow.MainModeratorRepository.UniversityMainModeratorExists(userId);
        }

        public IList<UniversityMainModerator> GetAllUniversitiesMainModerators()
        {
            return _uow.MainModeratorRepository.GetAllUniversitiesMainModerators();
        }
    }
}
