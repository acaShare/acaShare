using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator.Models.StructureManagement
{
    public class SemesterViewModel : IListItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Numer semestru")]
        public string TitleOrFullName { get; set; }

        // Not used
        public string SubtitleOrAbbreviation { get; set; }

        // helper variable
        public int DepartmentId { get; set; }
    }
}
