using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class File
    {
        public File(byte[] fileData) : this()
        {
            File1 = fileData;
        }

        protected File()
        {
        }

        public void AddToMaterial(Material material)
        {
            if (Material == null)
            {
                Material = material;
                material.AddFile(this);
            }
        }

        public int FileId { get; private set; }
        public int? MaterialId { get; private set; }
        public byte[] File1 { get; private set; }
        public int? EditRequestId { get; private set; }

        public virtual EditRequest EditRequest { get; private set; }
        public virtual Material Material { get; private set; }
    }
}
