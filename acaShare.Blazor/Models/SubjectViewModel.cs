﻿using System.ComponentModel.DataAnnotations;

namespace acaShare.Blazor.Models
{
    public class SubjectViewModel
    {
        public int LessonId { get; set; }
        public int SemesterId { get; set; }
        public int DepartmentId { get; set; }
        public int UniversityId { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [MaxLength(126, ErrorMessage = "Maksymalna długość pola \"{0}\" to {1} znaków")]
        [Display(Name = "Nazwa przedmiotu")]
        public string TitleOrFullName { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [MaxLength(5, ErrorMessage = "Maksymalna długość pola \"{0}\" to {1} znaków")]
        [Display(Name = "Skrót")]
        public string SubtitleOrAbbreviation { get; set; }

        public int MaterialsCount { get; set; }
    }
}