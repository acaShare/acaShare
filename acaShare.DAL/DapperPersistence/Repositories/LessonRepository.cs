using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace acaShare.DAL.DapperPersistence.Repositories
{
    public sealed class LessonRepository : DapperRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public new Lesson FindById(int lessonId)
        {
            Lesson lesson = _transaction.Connection.QuerySingle<Lesson>(
                $"SELECT * FROM Lesson WHERE LessonId = {lessonId}", 
                transaction: _transaction);

            SectionOfSubject sectionOfSubject = _transaction.Connection.QuerySingle<SectionOfSubject>(
                $"SELECT * FROM SectionOfSubject WHERE SectionOfSubjectId = {lesson.SectionOfSubjectId}",
                transaction: _transaction);

            Subject subject = _transaction.Connection.QuerySingle<Subject>(
                $"SELECT * FROM Subject WHERE SubjectId = {sectionOfSubject.SubjectId}",
                transaction: _transaction);

            Lecturer lecturer = _transaction.Connection.QuerySingle<Lecturer>(
                $"SELECT * FROM Lecturer WHERE LecturerId = {lesson.LecturerId}",
                transaction: _transaction);

            Semester semester = _transaction.Connection.QuerySingle<Semester>(
                $"SELECT * FROM Semester WHERE SemesterId = {lesson.SemesterId}",
                transaction: _transaction);

            AcademicYear academicYear = _transaction.Connection.QuerySingle<AcademicYear>(
                $"SELECT * FROM AcademicYear WHERE AcademicYearId = {semester.AcademicYearId}",
                transaction: _transaction);

            SemesterNumber semesterNumber = _transaction.Connection.QuerySingle<SemesterNumber>(
                $"SELECT * FROM SemesterNumber WHERE SemesterNumberId = {semester.SemesterNumberId}",
                transaction: _transaction);

            Department department = _transaction.Connection.QuerySingle<Department>(
                $"SELECT * FROM Department WHERE DepartmentId = {lesson.DepartmentId}",
                transaction: _transaction);

            University university = _transaction.Connection.QuerySingle<University>(
                $"SELECT * FROM University WHERE UniversityId = {department.UniversityId}",
                transaction: _transaction);

            // section of subject, subject
            subject.SectionsOfSubject.Add(sectionOfSubject);
            sectionOfSubject.Subject = subject;
            sectionOfSubject.Lessons.Add(lesson);
            lesson.SectionOfSubject = sectionOfSubject;

            // lecturer
            lecturer.Lessons.Add(lesson);
            lesson.Lecturer = lecturer;

            // semester, academic year, semester number
            academicYear.Semesters.Add(semester);
            semester.AcademicYear = academicYear;
            semesterNumber.Semesters.Add(semester);
            semester.SemesterNumber = semesterNumber;
            semester.Lessons.Add(lesson);
            lesson.Semester = semester;

            // department, university
            university.Departments.Add(department);
            department.University = university;
            lesson.Department = department;

            return lesson;
        }
    }
}
