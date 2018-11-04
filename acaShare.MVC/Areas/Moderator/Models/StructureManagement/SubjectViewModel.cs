using System.ComponentModel.DataAnnotations;

namespace acaShare.MVC.Areas.Moderator.Models.StructureManagement
{
    public class SubjectDepartmentViewModel : IListItemViewModel
    {
        // SubjectDepartmentId
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Nazwa przedmiotu")]
        public string TitleOrFullName { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Skrót")]
        public string SubtitleOrAbbreviation { get; set; }

        // Helper variables
        public int SemesterId { get; set; }
        public int DepartmentId { get; set; }
    }
}