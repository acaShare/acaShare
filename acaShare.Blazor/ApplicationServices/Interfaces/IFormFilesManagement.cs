using acaShare.BLL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace acaShare.Blazor.ApplicationServices.Interfaces
{
    public interface IFormFilesManagement
    {
        void RemoveFilesFromFileSystem(ICollection<File> filesToRemove);
        ICollection<File> ExtractFilesFromForm(ICollection<IFormFile> formFiles, int materialId, Guid guid, int? editRequestId = null);
        void SaveFilesToFileSystem(ICollection<IFormFile> formFiles, int materialId, Guid guid, int? editRequestId = null);
        string GetUploadFolderAbsolutePath();
        void ReplaceMaterialFilesWithEditRequestFiles(int materialId, int editRequestId, ICollection<File> files);
        bool ExistsInMaterial(IFormFile file, int materialId);
        void DeleteWholeMaterialFolder(int materialId);
    }
}
