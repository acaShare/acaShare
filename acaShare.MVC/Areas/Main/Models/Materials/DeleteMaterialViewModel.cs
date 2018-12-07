using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class DeleteMaterialViewModel
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public int LessonId { get; set; }
        public int FilesCount { get; set; }
    }
}
