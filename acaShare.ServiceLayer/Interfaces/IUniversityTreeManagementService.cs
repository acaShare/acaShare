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
        void AddUniversity(University university);
        void UpdateUniversity(University university);
        void DeleteUniversity(University university);

        // Department
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department department);

        // Subject
        void AddSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(int subjectId);

        // Lesson
        void AddLesson(Lesson lesson);
        void UpdateLesson(Lesson lesson);
        void DeleteLesson(int lessonId);
    }
}
