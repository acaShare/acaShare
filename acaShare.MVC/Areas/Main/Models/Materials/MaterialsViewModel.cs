using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class MaterialsViewModel
    {
        public ICollection<MaterialViewModel> Materials { get; set; }
        public bool IsWithSubtitles { get; set; }
        public int LessonId { get; set; }
    }
}
