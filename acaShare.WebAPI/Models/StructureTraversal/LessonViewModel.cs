using System.ComponentModel.DataAnnotations;

namespace acaShare.WebAPI.Models.StructureTraversal
{
    public class LessonViewModel : IListItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Nazwa wydziału")]
        public string TitleOrFullName { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Skrót")]
        public string SubtitleOrAbbreviation { get; set; }

        // Helper properties
        public int MaterialsCount { get; set; }
        public int SemesterId { get; set; }
        public int DepartmentId { get; set; }
    }
}
