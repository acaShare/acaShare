using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class EditRequest
    {
        public EditRequest()
        {
            Files = new HashSet<File>();
        }

        public int EditRequestId { get; set; }
        public int UpdaterId { get; set; }
        public int MaterialToUpdateId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Summary { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }

        public virtual Material MaterialToUpdate { get; set; }
        public virtual User Updater { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
