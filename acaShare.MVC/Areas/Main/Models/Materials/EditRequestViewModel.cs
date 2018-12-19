using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class EditRequestViewModel : IMaterialManagementViewModel, IEditViewModel
    {
        [Required]
        public EditMaterialViewModel EditMaterialViewModel { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        [Display(Name = "Podsumowanie zmian")]
        public string Summary { get; set; }

        // In an Edit requests we use the below FormFiles property to not mess with the javascript responsible for editing.
        [Display(Name = "Pliki")]
        public ICollection<IFormFile> FormFiles { get; set; }

        // In an Edit requests we use the below Files property (not these from EditMaterialViewModel)
        public ICollection<FileViewModel> Files { get; set; }

        public int StartingId { get; set; }
    }
}
