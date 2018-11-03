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

        public void DeleteDepartment(Department departmentToDelete)
        {
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

        public void DeleteUniversity(University universityToDelete)
        {
            _uow.Universities.Delete(universityToDelete);
            _uow.SaveChanges();
        }

        public Department GetDepartment(int id)
        {
            return _uow.Departments.FindById(id);
        }

        public List<Department> GetDepartments()
        {
            return _uow.Departments.GetAll();
        }

        public Lecturer GetLecturer(int id)
        {
            return _uow.Lecturers.FindById(id);
        }

        public List<Lecturer> GetLecturers()
        {
            return _uow.Lecturers.GetAll();
        }

        public Lesson GetLesson(int id)
        {
            return _uow.Lessons.FindById(id);
        }

        public List<Lesson> GetLessons()
        {
            return _uow.Lessons.GetAll();
        }

        public SectionOfSubject GetSectionOfSubject(int id)
        {
            return _uow.SectionsOfSubject.FindById(id);
        }

        public List<SectionOfSubject> GetSectionsOfSubject()
        {
            return _uow.SectionsOfSubject.GetAll();
        }

        public Subject GetSubject(int id)
        {
            return _uow.Subjects.FindById(id);
        }

        public List<Subject> GetSubjects()
        {
            return _uow.Subjects.GetAll();
        }

        public List<University> GetUniversities()
        {
            return _uow.Universities.GetAll();
        }

        public University GetUniversity(int id)
        {
            return _uow.Universities.FindById(id);
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
