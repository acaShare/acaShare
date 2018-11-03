using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator.Models.StructureManagement
{
    public class UniversityViewModel : IListItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Pełna nazwa uczelni")]
        public string TitleOrFullName { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Skrót")]
        public string SubtitleOrAbbreviation { get; set; }
    }
}
