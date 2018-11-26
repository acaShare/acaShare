using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class MaterialViewModel
    {
        public int MaterialId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Dodano")]
        public DateTime UploadDate { get; set; }

        [Display(Name = "Zmodyfikowano")]
        public DateTime? ModificationDate { get; set; }

        [Display(Name = "Status")]
        public string State { get; set; }

        [Display(Name = "Autor")]
        public string CreatorUsername { get; set; }

        [Display(Name = "przez")]
        public string UpdaterUsername { get; set; }

        public string ApproverUsername { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsAllowedToEditOrDelete { get; set; }
        public ICollection<FileViewModel> Files { get; set; }
    }
}
