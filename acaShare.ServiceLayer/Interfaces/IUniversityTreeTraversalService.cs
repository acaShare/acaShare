using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface IUniversityTreeTraversalService
    {
        Lesson GetLesson(int id);
        IEnumerable<Lesson> GetLessons();
        Lecturer GetLecturer(int id);
        IEnumerable<Lecturer> GetLecturers();
        SectionOfSubject GetSectionOfSubject(int id);
        IEnumerable<SectionOfSubject> GetSectionsOfSubject();
        Subject GetSubject(int id);
        List<Subject> GetSubjects();

        Department GetDepartment(int id);
        IEnumerable<Department> GetDepartmentsFromUniversity(int universityId);

        University GetUniversity(int id);
        IEnumerable<University> GetUniversities();
    }
}
