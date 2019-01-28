using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories.UniversityRelated
{
    public interface IUniversityRepository : IRepository<University>
    {
        IEnumerable<Department> GetDepartmentsFromUniversity(int universityId);
        bool DoesUniversityAlreadyExist(University university);
    }
}
