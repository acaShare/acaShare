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
        void DeleteUniversity(int universityId);

        // Department
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(int departmentId);

        // Subject
        void AddSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(int subjectId);

        // SectionOfSubject
        void AddSectionOfSubject(SectionOfSubject sectionOfSubject);
        void UpdateSectionOfSubject(SectionOfSubject sectionOfSubject);
        void DeleteSectionOfSubject(int sectionOfSubjectId);

        // Lecturer
        void AddLecturer(Lecturer lecturer);
        void UpdateLecturer(Lecturer lecturer);
        void DeleteLecturer(int lecturerId);

        // Lesson
        void AddLesson(Lesson lesson);
        void UpdateLesson(Lesson lesson);
        void DeleteLesson(int lessonId);
    }
}
