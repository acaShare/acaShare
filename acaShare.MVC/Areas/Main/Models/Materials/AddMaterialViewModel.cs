using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class AddMaterialViewModel
    {
        public int LessonId { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Pliki")]
        public IEnumerable<byte[]> Files { get; set; }
    }
}
