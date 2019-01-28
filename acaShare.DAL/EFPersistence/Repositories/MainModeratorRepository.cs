using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class MainModeratorRepository : IMainModeratorRepository
    {
        private readonly DbSet<UniversityMainModerator> _mainModerators;

        public MainModeratorRepository(DbSet<UniversityMainModerator> mainModerators)
        {
            _mainModerators = mainModerators;
        }

        public void AssignMainModeratorToUniversity(UniversityMainModerator universityMainModerator)
        {
            _mainModerators.Add(universityMainModerator);
        }

        public void UnassignMainModeratorFromUniversity(UniversityMainModerator universityMainModerator)
        {
            _mainModerators.Remove(universityMainModerator);
        }

        public UniversityMainModerator GetUniversityMainModerator(int userId)
        {
            return _mainModerators.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public void EditMainModeratorAssignement(UniversityMainModerator universityMainModerator)
        {
            var recordToDelete = _mainModerators.Where(x => x.UserId == universityMainModerator.UserId).First();

            _mainModerators.Remove(recordToDelete);

            _mainModerators.Add(universityMainModerator);
        }

        public bool UniversityMainModeratorExists(int userId)
        {
            var mod = _mainModerators.Where(x => x.UserId == userId).FirstOrDefault();

            if(mod == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IList<UniversityMainModerator> GetAllUniversitiesMainModerators()
        {
            return _mainModerators.ToList();
        }
    }
}
