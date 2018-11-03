using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator.Models.StructureManagement
{
    public class DepartmentViewModel : IListItemViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Nazwa wydziału")]
        public string TitleOrFullName { get; set; }

        // Not used
        public string SubtitleOrAbbreviation { get; set; }

        // helper variable
        public int UniversityId { get; set; }
    }
}
