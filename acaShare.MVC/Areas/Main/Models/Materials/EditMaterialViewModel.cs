using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class EditMaterialViewModel
    {
        public int MaterialId { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [MaxLength(255, ErrorMessage = "{0} materiału nie może przekraczać {1} znaków")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        
        [MaxLength(4000, ErrorMessage = "{0} materiału nie może przekraczać {1} znaków")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Pliki")]
        public ICollection<IFormFile> FormFiles { get; set; }

        [Display(Name = "Pliki")]
        public ICollection<FileViewModel> Files { get; set; }
    }
}
