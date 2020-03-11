using System;
using System.ComponentModel.DataAnnotations;

namespace acaShare.WebAPI.Models.MaterialChangeRequests
{
    public class DeleteRequestViewModel
    {
        [Required]
        public int DeleteRequestId { get; set; }
        public DateTime RequestDate { get; set; }

        [Display(Name = "Nazwa materiału")]
        public string MaterialName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ReasonId { get; set; }

        [Display(Name = "Powód")]
        public string Reason { get; set; }

        [Display(Name = "Dodatkowy komentarz")]
        public string AdditionalComment { get; set; }

        [Display(Name = "Sugestia utworzona przez")]
        public string DeleterName { get; set; }

        [Required(ErrorMessage = "{0} jest wymagany")]
        [MaxLength(1000, ErrorMessage = "Maksymalna długość pola \"{0}\" to {1} znaków")]
        [Display(Name = "Powód odrzucenia sugestii")]
        public string DeclineReason { get; set; }
    }
}
