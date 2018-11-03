﻿using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface IUniversityTreeTraversalService
    {
        Lesson GetLesson(int id);
        IEnumerable<Lesson> GetLessons();
        Subject GetSubject(int id);
        List<Subject> GetSubjects();

        Department GetDepartment(int id);
        IEnumerable<Department> GetDepartmentsFromUniversity(int universityId);

        University GetUniversity(int id);
        IEnumerable<University> GetUniversities();
    }
}
