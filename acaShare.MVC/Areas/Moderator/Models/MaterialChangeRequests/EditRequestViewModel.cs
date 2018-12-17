using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator.Models.MaterialChangeRequests
{
    public class EditRequestViewModel
    {
        [Required]
        public int EditRequestId { get; set; }
        public string MaterialName { get; set; }
        public DateTime RequestDate { get; set; }

        [Display(Name = "Podsumowanie zmian")]
        public string Summary { get; set; }

        [Display(Name = "Sugestia utworzona przez")]
        public string UpdaterName { get; set; }

        public ICollection<FileViewModel> Files { get; set; }

        [Display(Name = "Powód odrzucenia sugestii")]
        public string DeclineReason { get; set; }
    }
}
