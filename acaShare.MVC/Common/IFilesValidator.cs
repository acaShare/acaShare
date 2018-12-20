using acaShare.MVC.Areas.Main.Models.Materials;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Common
{
    public interface IFilesValidator
    {
        int MaxFileNameLength { get; set; }
        bool AreUploadedFilesValid(ICollection<IFormFile> formFiles);
        bool IsNotValidUploadFile(IFormFile file);
        bool HasFileName(IFormFile file);
        bool HasFileNameRequiredLength(string fileName);
        bool AlreadyExistsInMaterial(IFormFile formFile, int materialId);
        ICollection<string> ValidateFileNames(ICollection<IFormFile> formFiles, ICollection<FileViewModel> newFiles);
    }
}
