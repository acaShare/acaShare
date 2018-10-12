using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class File
    {
        public int FileId { get; set; }
        public int? MaterialId { get; set; }
        public byte[] File1 { get; set; }
        public int? EditRequestId { get; set; }

        public virtual EditRequest EditRequest { get; set; }
        public virtual Material Material { get; set; }
    }
}
