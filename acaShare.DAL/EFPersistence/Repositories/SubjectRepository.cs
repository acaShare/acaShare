using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UniversityRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public sealed class SubjectRepository : EFRepository<Subject>, ISubjectRepository
    {
        private readonly DbSet<SubjectDepartment> _subjectDepartments;

        public SubjectRepository(DbSet<Subject> dbSet, DbSet<SubjectDepartment> subjectDepartments) : base(dbSet)
        {
            _subjectDepartments = subjectDepartments;
        }

        public Subject GetByName(string name)
        {
            return _dbSet.FirstOrDefault(s => s.Name == name);
        }

        public SubjectDepartment GetSubDept(Subject subject)
        {
            return _subjectDepartments.FirstOrDefault(sd => 
                sd.Subject.Name == subject.Name && sd.Department.Name == subject.SubjectDepartment.First().Department.Name);
        }
    }
}
