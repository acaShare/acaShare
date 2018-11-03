﻿using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
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

        public IEnumerable<University> GetUniversities()
        {
            return _uow.Universities.GetAll();
        }

        public IEnumerable<Department> GetDepartmentsFromUniversity(int universityId)
        {
            return _uow.Universities.GetDepartmentsFromUniversity(universityId);
        }


        public University GetUniversity(int id)
        {
            return _uow.Universities.FindById(id);
        }

        public Department GetDepartment(int id)
        {
            return _uow.Departments.FindById(id);
        }



        public Lecturer GetLecturer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lecturer> GetLecturers()
        {
            throw new NotImplementedException();
        }

        public Lesson GetLesson(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lesson> GetLessons()
        {
            throw new NotImplementedException();
        }

        public SectionOfSubject GetSectionOfSubject(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SectionOfSubject> GetSectionsOfSubject()
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