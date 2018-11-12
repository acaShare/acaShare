﻿using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories.UniversityRelated
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        IEnumerable<SubjectDepartment> FindSubjectDepartmentAssociations(int departmentId);
    }
}
