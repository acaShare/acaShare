using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class LessonViewModel
    {
        public int LessonId { get; set; }
        public int SemesterId { get; set; }
        public int SubjectDepartmentId { get; set; }
    }
}
