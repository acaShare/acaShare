using acaShare.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace acaShare.WebAPI.Common
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
