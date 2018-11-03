using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Subject
    {
        protected Subject()
        {
            SubjectDepartment = new HashSet<SubjectDepartment>();
        }

        public int SubjectId { get; private set; }
        public string Name { get; private set; }

        public virtual ICollection<SubjectDepartment> SubjectDepartment { get; private set; }
    }
}
