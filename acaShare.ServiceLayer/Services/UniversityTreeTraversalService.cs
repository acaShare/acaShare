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
        

        public IEnumerable<Lesson> GetLessons(Semester semester, Department department)
        {
            return _uow.Lessons.GetLessonsFromSemesterInDepartment(semester, department);
        }

        public Lesson GetLesson(int id)
        {
            return _uow.Lessons.FindById(id);
        }
    }
}
