using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    // These functionalites will probably be separated into dedicated classes
    public interface IUniversityTreeManagementService
    {
        // University
        bool AddUniversity(University university);
        bool UpdateUniversity(University university);
        void DeleteUniversity(University university);

        // Department
        bool AddDepartment(Department department);
        bool UpdateDepartment(Department department);
        void DeleteDepartment(Department department);

        // Subject
        Subject AddSubjectIfNotExistsOrGetOtherwise(Subject subject);

        // Lesson
        bool AddLesson(Lesson lesson);
        bool UpdateLesson(int lessonId, string subjectName, string subjectAbbreviation);
        void DeleteLesson(int lessonId);
    }
}
