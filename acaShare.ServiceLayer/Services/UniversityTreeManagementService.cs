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

        public bool AddDepartment(Department department)
        {
            if (_uow.Departments.DoesDepartmentAlreadyExistInUniversity(department.Name, department.University.Name))
            {
                return false;
            }
           
            if (_uow.Departments.IsAbbreviationAlreadyTaken(department.University.Name, department.Abbreviation))
            {
                return false;
            }

            _uow.Departments.Add(department);
            _uow.SaveChanges();

            return true;
        }

        public bool AddLesson(Lesson lesson, string abbreviation, SubjectDepartment subjectDepartment)
        {
            if (_uow.Lessons.IsAbbreviationAlreadyTaken(subjectDepartment.DepartmentId, lesson.SemesterId, abbreviation))
            {
                return false;
            }

            if (_uow.Lessons.DoesLessonAlreadyExist(lesson.SubjectDepartmentId, lesson.SemesterId))
            {
                return false;
            }

            _uow.Lessons.Add(lesson);
            _uow.SaveChanges();

            return true;
        }

        public SubjectDepartment AddSubject(Subject subject)
        {
            var existingSubject = _uow.Subjects.GetByName(subject.Name);
            if (existingSubject != null)
            {
                var subDept = _uow.Subjects.GetSubDept(subject);
                if (subDept != null)
                {
                    return subDept;
                }
                else
                {
                    subDept = subject.SubjectDepartment.First();
                    existingSubject.SubjectDepartment.Add(subDept);
                    _uow.SaveChanges();
                    return subDept;
                }
            }

            _uow.Subjects.Add(subject);
            _uow.SaveChanges();

            return subject.SubjectDepartment.First();
        }

        public bool AddUniversity(University university)
        {
            if (_uow.Universities.DoesUniversityAlreadyExist(university.Name))
            {
                return false;
            }

            if (_uow.Universities.IsAbbreviationAlreadyTaken(university.Abbreviation))
            {
                return false;
            }

            _uow.Universities.Add(university);
            _uow.SaveChanges();

            return true;
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
