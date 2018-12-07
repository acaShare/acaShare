using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class File
    {
        public File(int fileId, string fileName)
        {
            FileId = fileId;
            FileName = fileName;
        }

        public File(string fileName, string fileRelativePath, string contentType) : this()
        {
            FileName = fileName;
            RelativePath = fileRelativePath;
            ContentType = contentType.ToLower();
        }

        protected File()
        {
        }

        public void AddToMaterial(Material material)
        {
            if (Material == null && material != null)
            {
                Material = material;
                material.AddFile(this);
            }
        }

        public int FileId { get; private set; }
        public int? MaterialId { get; private set; }
        public string RelativePath { get; private set; }
        public string FileName { get; private set; }
        public string ContentType { get; private set; }
        public int? EditRequestId { get; private set; }

        public virtual EditRequest EditRequest { get; private set; }
        public virtual Material Material { get; private set; }

        public void Update(string fileName)
        {
            FileName = fileName;
        }
    }
}
