using System;
using System.Collections.Generic;

namespace acaShare.Blazor.Models
{
    public class MaterialToApproveViewModel
    {
        public int MaterialId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime UploadDate { get; set; }

        public string CreatorUsername { get; set; }

        public ICollection<FileViewModel> Files { get; set; }
    }
}
