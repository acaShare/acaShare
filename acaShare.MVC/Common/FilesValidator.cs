using acaShare.MVC.Areas.Main.Models.Materials;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Common
{
    public class FilesValidator : IFilesValidator
    {
        private readonly IFormFilesManagement _filesManagement;
        public int MaxFileNameLength { get; set; }

        public FilesValidator(IFormFilesManagement filesManagement)
        {
            _filesManagement = filesManagement;
        }

        public bool AreUploadedFilesValid(ICollection<IFormFile> formFiles)
        {
            return !formFiles?.Any(f => IsNotValidUploadFile(f)) ?? true;
        }

        public bool IsNotValidUploadFile(IFormFile file)
        {
            return !HasFileName(file) || !HasFileNameRequiredLength(file.FileName);;
        }

        public bool HasFileName(IFormFile file)
        {
            return !string.IsNullOrEmpty(file.FileName) && !string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(file.FileName));
        }

        public bool HasFileNameRequiredLength(string fileName)
        {
            return fileName.Length <= MaxFileNameLength;
        }

        // Not used
        public bool AlreadyExistsInMaterial(IFormFile file, int materialId)
        {
            return _filesManagement.ExistsInMaterial(file, materialId);
        }

        public ICollection<string> ValidateFileNames(ICollection<IFormFile> formFiles, ICollection<FileViewModel> updatedExistingFiles)
        {
            var repeatedFileNames = new List<string>();
            foreach (var newFile in updatedExistingFiles ?? Enumerable.Empty<FileViewModel>())
            {
                var file = updatedExistingFiles.FirstOrDefault(f => f.FileName == newFile.FileName && f != newFile);
                if (file != null)
                {
                    repeatedFileNames.Add(newFile.FileName);
                }
            }

            foreach (var formFile in formFiles ?? Enumerable.Empty<IFormFile>())
            {
                var file = formFiles.FirstOrDefault(f => f.FileName == formFile.FileName && f != formFile);
                if (file != null)
                {
                    repeatedFileNames.Add(file.FileName);
                }

                var file2 = updatedExistingFiles?.FirstOrDefault(f => f.FileName == Path.GetFileNameWithoutExtension(formFile.FileName));
                if (file2 != null)
                {
                    repeatedFileNames.Add(file2.FileName);
                }
            }

            return repeatedFileNames;
        }
    }
}
