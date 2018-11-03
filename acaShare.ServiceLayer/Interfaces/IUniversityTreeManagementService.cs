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
        University GetUniversity(int id);
        List<University> GetUniversities();
        void AddUniversity(University university);
        void UpdateUniversity(University university);
        void DeleteUniversity(University university);

        // Department
        Department GetDepartment(int id);
        List<Department> GetDepartments();
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department department);

        // Subject
        Subject GetSubject(int id);
        List<Subject> GetSubjects();
        void AddSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(int subjectId);

        // SectionOfSubject
        SectionOfSubject GetSectionOfSubject(int id);
        List<SectionOfSubject> GetSectionsOfSubject();
        void AddSectionOfSubject(SectionOfSubject sectionOfSubject);
        void UpdateSectionOfSubject(SectionOfSubject sectionOfSubject);
        void DeleteSectionOfSubject(int sectionOfSubjectId);

        // Lecturer
        Lecturer GetLecturer(int id);
        List<Lecturer> GetLecturers();
        void AddLecturer(Lecturer lecturer);
        void UpdateLecturer(Lecturer lecturer);
        void DeleteLecturer(int lecturerId);

        // Lesson
        Lesson GetLesson(int id);
        List<Lesson> GetLessons();
        void AddLesson(Lesson lesson);
        void UpdateLesson(Lesson lesson);
        void DeleteLesson(int lessonId);
    }
}
