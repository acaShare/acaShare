using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator.Models.MaterialChangeRequests
{
    public class DeleteRequestViewModel
    {
        [Required]
        public int DeleteRequestId { get; set; }

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

        [Display(Name = "Powód odrzucenia sugestii")]
        public string DeclineReason { get; set; }
    }
}
