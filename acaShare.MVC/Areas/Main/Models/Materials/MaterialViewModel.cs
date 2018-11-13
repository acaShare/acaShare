using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class MaterialViewModel
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public UserViewModel Approver { get; set; }
        public UserViewModel Creator { get; set; }
        public LessonViewModel Lesson { get; set; }
        public MaterialState State { get; set; }
        public UserViewModel Updater { get; set; }
    }
}
