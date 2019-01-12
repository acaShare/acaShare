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
        bool AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department department);

        // Subject
        SubjectDepartment AddSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(int subjectId);

        // Lesson
        bool AddLesson(Lesson lesson, string abbreviation, SubjectDepartment subjectDepartment);
        void UpdateLesson(Lesson lesson);
        void DeleteLesson(int lessonId);
    }
}
