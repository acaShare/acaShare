using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface IUniversityTreeTraversalService
    {
        University GetUniversity(int id);
        IEnumerable<University> GetUniversities();

        Department GetDepartment(int id);
        IEnumerable<Department> GetDepartmentsFromUniversity(int universityId);

        IEnumerable<Semester> GetSemesters();
        Semester GetSemester(int semesterId);

        Lesson GetLesson(int id);
        IEnumerable<Lesson> GetLessons(Semester semester, Department department);

        IEnumerable<Material> GetMaterialsFromLesson(int lessonId);
    }
}
