using System.ComponentModel.DataAnnotations;

namespace acaShare.WebAPI.Models.StructureTraversal
{
    public class SemesterViewModel : IListItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Numer semestru")]
        public string Name { get; set; }

        // Not used
        public string Abbreviation { get; set; }

        // helper variable
        public int DepartmentId { get; set; }
    }
}
