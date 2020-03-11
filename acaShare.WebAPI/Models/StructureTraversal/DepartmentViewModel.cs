﻿using System.ComponentModel.DataAnnotations;

namespace acaShare.WebAPI.Models.StructureTraversal
{
    public class DepartmentViewModel : IListItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [MaxLength(100, ErrorMessage = "Maksymalna długość pola \"{0}\" to {1} znaków")]
        [Display(Name = "Nazwa wydziału")]
        public string TitleOrFullName { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [MaxLength(5, ErrorMessage = "Maksymalna długość pola \"{0}\" to {1} znaków")]
        [Display(Name = "Skrót")]
        public string SubtitleOrAbbreviation { get; set; }

        public int UniversityId { get; set; }
    }
}
