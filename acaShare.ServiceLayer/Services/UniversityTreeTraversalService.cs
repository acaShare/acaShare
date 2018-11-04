using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    public class UniversityTreeTraversalService : IUniversityTreeTraversalService
    {
        private readonly IUnitOfWork _uow;

        public UniversityTreeTraversalService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public University GetUniversity(int id)
        {
            return _uow.Universities.FindById(id);
        }

        public IEnumerable<University> GetUniversities()
        {
            return _uow.Universities.GetAll();
        }


        public Department GetDepartment(int id)
        {
            return _uow.Departments.FindById(id);
        }

        public IEnumerable<Department> GetDepartmentsFromUniversity(int universityId)
        {
            return _uow.Universities.GetDepartmentsFromUniversity(universityId);
        }


        public Semester GetSemester(int semesterId)
        {
            return _uow.Semesters.FindById(semesterId);
        }

        public IEnumerable<Semester> GetSemesters()
        {
            return _uow.Semesters.GetAll();
        }



        public IEnumerable<SubjectDepartment> GetSubjectDepartmentAssociationResultsForDepartment(int departmentId)
        {
            return _uow.Departments.FindSubjectDepartmentAssociations(departmentId);
        }


        public IEnumerable<Lesson> GetLessons(int semesterId, IEnumerable<SubjectDepartment> subjectDepartmentAssociationResults)
        {
            return _uow.Lessons.GetAll().Where(l => l.SemesterId == semesterId && subjectDepartmentAssociationResults.Contains(l.SubjectDepartment));
        }


        public Lesson GetLesson(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lesson> GetLessons()
        {
            throw new NotImplementedException();
        }

      

        public Subject GetSubject(int id)
        {
            throw new NotImplementedException();
        }

        public List<Subject> GetSubjects()
        {
            throw new NotImplementedException();
        }
    }
}
