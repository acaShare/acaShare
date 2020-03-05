using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace acaShare.Blazor.Models
{
    public class EditRequestViewModel
    {
        public int EditRequestId { get; set; }
        public string MaterialName { get; set; }
        public DateTime RequestDate { get; set; }
        public string NewDescription { get; set; }
        public string Summary { get; set; }
        public string UpdaterName { get; set; }
        public ICollection<FileViewModel> Files { get; set; }

        [Display(Name = "Powód odrzucenia sugestii")]
        public string DeclineReason { get; set; }
    }
}
