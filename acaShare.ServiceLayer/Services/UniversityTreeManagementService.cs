using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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



        public bool AddUniversity(University university)
        {
            if (_uow.Universities.DoesUniversityAlreadyExist(university))
            {
                return false;
            }

            _uow.Universities.Add(university);
            _uow.SaveChanges();

            return true;
        }

        public bool AddDepartment(Department department)
        {
            if (_uow.Departments.DoesDepartmentAlreadyExistInUniversity(department))
            {
                return false;
            }

            _uow.Departments.Add(department);
            _uow.SaveChanges();

            return true;
        }

        public bool AddLesson(Lesson lesson)
        {
            var subject = AddSubjectIfNotExistsOrGetOtherwise(lesson.Subject);

            lesson.ReplaceSubjectForNew(subject);

            if (subject.IsInLesson(lesson))
            {
                return false;
            }

            if (_uow.Lessons.IsSubjectWithSameNameOrAbbreviationExistInDepartment(lesson))
            {
                return false;
            }
           
            _uow.Lessons.Add(lesson);
            _uow.SaveChanges();

            return true;
        }

        public Subject AddSubjectIfNotExistsOrGetOtherwise(Subject subject)
        {
            var existingSubject = _uow.Subjects.GetByNaming(subject.Name, subject.Abbreviation);
            if (existingSubject != null)
            {
                return existingSubject;
            }

            _uow.Subjects.Add(subject);
            _uow.SaveChanges();

            return subject;
        }



        public void DeleteUniversity(University universityToDelete)
        {
            _uow.Universities.Delete(universityToDelete);
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



        public bool UpdateUniversity(University university)
        {
            if (_uow.Universities.DoesUniversityAlreadyExist(university))
            {
                return false;
            }

            _uow.Universities.Update(university);
            _uow.SaveChanges();

            return true;
        }

        public bool UpdateDepartment(Department department)
        {
            if (_uow.Departments.DoesDepartmentAlreadyExistInUniversity(department))
            {
                return false;
            }

            _uow.Departments.Update(department);
            _uow.SaveChanges();

            return true;
        }

        public bool UpdateLesson(Lesson lesson)
        {
            var subject = AddSubjectIfNotExistsOrGetOtherwise(lesson.Subject);

            lesson.ReplaceSubjectForNew(subject);

            if (subject.IsInLesson(lesson))
            {
                return false;
            }

            if (_uow.Lessons.IsSubjectWithSameNameOrAbbreviationExistInDepartment(lesson))
            {
                return false;
            }
            
            _uow.Lessons.Update(lesson);
            _uow.SaveChanges();

            return true;
        }
    }
}
