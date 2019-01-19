using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface IMainModeratorService
    {
        void AssignMainModeratorToUniversity(UniversityMainModerator universityMainModerator);
        void EditMainModeratorAssignement(UniversityMainModerator universityMainModerator);
        void UnassignMainModeratorFromUniversity(UniversityMainModerator universityMainModerator);
        bool UniversityMainModeratorExists(int userId);
        UniversityMainModerator GetUniversityMainModerator(int userId);
        IList<UniversityMainModerator> GetAllUniversitiesMainModerators();
    }
}
