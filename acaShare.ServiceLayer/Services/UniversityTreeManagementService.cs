using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    // These functionalites will probably be separated into dedicated classes
    public class UniversityTreeManagementService : IUniversityTreeManagementService
    {
        private readonly IUnitOfWork _uow;

        public UniversityTreeManagementService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddDepartment(Department department)
        {
            _uow.Departments.Add(department);
            _uow.SaveChanges();
        }

        public void AddLecturer(Lecturer lecturer)
        {
            _uow.Lecturers.Add(lecturer);
            _uow.SaveChanges();
        }

        public void AddLesson(Lesson lesson)
        {
            _uow.Lessons.Add(lesson);
            _uow.SaveChanges();
        }

        public void AddSectionOfSubject(SectionOfSubject sectionOfSubject)
        {
            _uow.SectionsOfSubject.Add(sectionOfSubject);
            _uow.SaveChanges();
        }

        public void AddSubject(Subject subject)
        {
            _uow.Subjects.Add(subject);
            _uow.SaveChanges();
        }

        public void AddUniversity(University university)
        {
            _uow.Universities.Add(university);
            _uow.SaveChanges();
        }

        public void DeleteDepartment(int departmentId)
        {
            var departmentToDelete = _uow.Departments.FindById(departmentId);
            _uow.Departments.Delete(departmentToDelete);
            _uow.SaveChanges();
        }

        public void DeleteLecturer(int lecturerId)
        {
            var lecturerToDelete = _uow.Lecturers.FindById(lecturerId);
            _uow.Lecturers.Delete(lecturerToDelete);
            _uow.SaveChanges();
        }

        public void DeleteLesson(int lessonId)
        {
            var lessonToDelete = _uow.Lessons.FindById(lessonId);
            _uow.Lessons.Delete(lessonToDelete);
            _uow.SaveChanges();
        }

        public void DeleteSectionOfSubject(int sectionOfSubjectId)
        {
            var sectionOfSubjectToDelete = _uow.SectionsOfSubject.FindById(sectionOfSubjectId);
            _uow.SectionsOfSubject.Delete(sectionOfSubjectToDelete);
            _uow.SaveChanges();
        }

        public void DeleteSubject(int subjectId)
        {
            var subjectToDelete = _uow.Subjects.FindById(subjectId);
            _uow.Subjects.Delete(subjectToDelete);
            _uow.SaveChanges();
        }

        public void DeleteUniversity(int universityId)
        {
            var universityToDelete = _uow.Universities.FindById(universityId);
            _uow.Universities.Delete(universityToDelete);
            _uow.SaveChanges();
        }

        public void UpdateDepartment(Department department)
        {
            _uow.Departments.Update(department);
            _uow.SaveChanges();
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            _uow.Lecturers.Update(lecturer);
            _uow.SaveChanges();
        }

        public void UpdateLesson(Lesson lesson)
        {
            _uow.Lessons.Update(lesson);
            _uow.SaveChanges();
        }

        public void UpdateSectionOfSubject(SectionOfSubject sectionOfSubject)
        {
            _uow.SectionsOfSubject.Update(sectionOfSubject);
            _uow.SaveChanges();
        }

        public void UpdateSubject(Subject subject)
        {
            _uow.Subjects.Update(subject);
            _uow.SaveChanges();
        }

        public void UpdateUniversity(University university)
        {
            _uow.Universities.Update(university);
            _uow.SaveChanges();
        }
    }
}
