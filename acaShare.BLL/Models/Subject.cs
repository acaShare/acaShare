using System;
using System.Collections.Generic;
using System.Linq;

namespace acaShare.BLL.Models
{
    public partial class Subject
    {
        public Subject(string name, string abbreviation) : this()
        {
            Name = name;
            Abbreviation = abbreviation;
        }

        protected Subject()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int SubjectId { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }

        public virtual ICollection<Lesson> Lessons { get; private set; }

        public void Update(string newName, string newAbbreviation)
        {
            Name = newName;
            Abbreviation = newAbbreviation;
        }

        public bool IsInLesson(Lesson lesson)
        {
            return Lessons.Any(l => l.SemesterId == lesson.SemesterId && l.SubjectId == lesson.SubjectId && l.DepartmentId == lesson.DepartmentId);
        }
    }
}
