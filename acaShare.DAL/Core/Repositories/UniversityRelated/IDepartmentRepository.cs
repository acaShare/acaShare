using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories.UniversityRelated
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        IEnumerable<SubjectDepartment> FindSubjectDepartmentAssociations(int departmentId);
        Department GetDepartmentByName(string deptName);
        bool DoesDepartmentAlreadyExistInUniversity(string deptName, string univName);
        bool IsAbbreviationAlreadyTaken(string univName, string abbreviation);
    }
}
