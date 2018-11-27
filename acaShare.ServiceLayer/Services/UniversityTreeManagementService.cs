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

        public void AddLesson(Lesson lesson)
        {
            _uow.Lessons.Add(lesson);
            _uow.SaveChanges();
        }

        public Subject AddSubject(Subject subject)
        {
            var result = _uow.Subjects.AddSubjectWithResult(subject);
            _uow.SaveChanges();

            return result;
        }

        public int AddSubjectWithResult(Subject subject)
        {
            //int subjectDepartmentId = _uow.Subjects.AddSubjectWithResult(subject);
            //_uow.SaveChanges();

            //return subjectDepartmentId;
            return -1;
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
        
        public void DeleteLesson(int lessonId)
        {
            var lessonToDelete = _uow.Lessons.FindById(lessonId);
            _uow.Lessons.Delete(lessonToDelete);
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


        public void UpdateDepartment(Department department)
        {
            _uow.Departments.Update(department);
            _uow.SaveChanges();
        }

        public void UpdateLesson(Lesson lesson)
        {
            _uow.Lessons.Update(lesson);
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
