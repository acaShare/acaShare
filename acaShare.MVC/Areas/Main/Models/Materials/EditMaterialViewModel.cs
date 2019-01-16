using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class EditMaterialViewModel : IMaterialManagementViewModel, IEditViewModel
    {
        public int MaterialId { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [MaxLength(80, ErrorMessage = "{0} materiału nie może przekraczać {1} znaków")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [MaxLength(4000, ErrorMessage = "{0} materiału nie może przekraczać {1} znaków")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Pliki")]
        public ICollection<IFormFile> FormFiles { get; set; }

        [Display(Name = "Pliki")]
        public ICollection<FileViewModel> Files { get; set; }

        public int StartingId { get; set; }
    }
}
