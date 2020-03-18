using System.ComponentModel.DataAnnotations;

namespace acaShare.WebAPI.Models.StructureTraversal
{
    public class SubjectDepartmentViewModel : IListItemViewModel
    {
        // SubjectDepartmentId
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [MaxLength(200, ErrorMessage = "Maksymalna długość pola \"{0}\" to {1} znaków")]
        [Display(Name = "Nazwa przedmiotu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [MaxLength(5, ErrorMessage = "Maksymalna długość pola \"{0}\" to {1} znaków")]
        [Display(Name = "Skrót")]
        public string Abbreviation { get; set; }

        // Helper variables
        public int SemesterId { get; set; }
        public int DepartmentId { get; set; }
    }
}