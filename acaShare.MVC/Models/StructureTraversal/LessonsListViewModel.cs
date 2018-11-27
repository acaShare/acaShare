using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Models.StructureTraversal
{
    public class LessonsListViewModel
    {
        public ICollection<LessonViewModel> Items { get; set; }
        public bool IsWithSubtitles { get; set; }
        public int SemesterId { get; set; }
        public int DepartmentId { get; set; }
    }
}
