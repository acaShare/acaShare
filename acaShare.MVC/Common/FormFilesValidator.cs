using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Common
{
    public static class FormFilesValidator
    {
        private static int MaxFileNameLength { get; set; }

        public static bool AreUploadedFilesValid(ICollection<IFormFile> formFiles, int maxFileNameLength)
        {
            MaxFileNameLength = maxFileNameLength;
            return !formFiles?.Any(f => IsNotValidUploadFile(f)) ?? true;
        }

        public static bool IsNotValidUploadFile(IFormFile file)
        {
            return !HasFileName(file) || !HasFileNameRequiredLength(file.FileName);
        }

        public static bool HasFileName(IFormFile file)
        {
            return !string.IsNullOrEmpty(file.FileName) && !string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(file.FileName));
        }

        public static bool HasFileNameRequiredLength(string fileName)
        {
            return fileName.Length <= MaxFileNameLength;
        }
    }
}
