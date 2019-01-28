using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public class Lesson
    {
        public Lesson(int semesterId, Department department, string fullName, string abbreviation) : this()
        {
            SemesterId = semesterId;
            Subject = new Subject(fullName, abbreviation);
            Department = department;
            DepartmentId = department.DepartmentId;
        }

        protected Lesson()
        {
            Materials = new HashSet<Material>();
        }

        public int LessonId { get; private set; }
        public int SemesterId { get; private set; }
        public int SubjectId { get; private set; }
        public int DepartmentId { get; private set; }

        public virtual Semester Semester { get; private set; }
        public virtual Subject Subject { get; private set; }
        public virtual Department Department { get; private set; }
        public virtual ICollection<Material> Materials { get; private set; }

        public int MaterialsCount => Materials.Count;

        public void Update(string newName, string newAbbreviation)
        {
            Subject.Update(newName, newAbbreviation);
        }

        public void ReplaceSubjectForNew(Subject subject)
        {
            Subject = subject;
            SubjectId = subject.SubjectId;
        }
    }
}
