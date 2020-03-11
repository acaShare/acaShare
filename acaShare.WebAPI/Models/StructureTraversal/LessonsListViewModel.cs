using System.Collections.Generic;

namespace acaShare.WebAPI.Models.StructureTraversal
{
    public class LessonsListViewModel
    {
        public ICollection<LessonViewModel> Items { get; set; }
        public bool IsWithSubtitles { get; set; }
        public int SemesterId { get; set; }
        public int DepartmentId { get; set; }
    }
}
