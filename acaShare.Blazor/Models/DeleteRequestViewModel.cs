using System;
using System.ComponentModel.DataAnnotations;

namespace acaShare.Blazor.Models
{
    public class DeleteRequestViewModel
    {
        public int DeleteRequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public string MaterialName { get; set; }
        public int ReasonId { get; set; }
        public string Reason { get; set; }
        public string AdditionalComment { get; set; }
        public string DeleterName { get; set; }

        [Required(ErrorMessage = "{0} jest wymagany")]
        [MaxLength(1000, ErrorMessage = "Maksymalna długość pola \"{0}\" to {1} znaków")]
        [Display(Name = "Powód odrzucenia sugestii")]
        public string DeclineReason { get; set; }
    }
}
