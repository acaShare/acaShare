using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories
{
    public interface IMainModeratorRepository
    {
        void AssignMainModeratorToUniversity(UniversityMainModerator universityMainModerator);
        void UnassignMainModeratorFromUniversity(UniversityMainModerator universityMainModerator);
        void EditMainModeratorAssignement(UniversityMainModerator universityMainModerator);
        bool UniversityMainModeratorExists(int userId);
        UniversityMainModerator GetUniversityMainModerator(int userId);
        IList<UniversityMainModerator> GetAllUniversitiesMainModerators();
    }
}
