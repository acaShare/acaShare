using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator.Models
{
    public class MaterialToApproveViewModel
    {
        public int MaterialId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Dodano")]
        public DateTime UploadDate { get; set; }

        [Display(Name = "Autor")]
        public string CreatorUsername { get; set; }

        public ICollection<FileViewModel> Files { get; set; }
    }
}
