using acaShare.BLL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Common
{
    public interface IFormFilesManagement
    {
        void RemoveFilesFromFileSystem(ICollection<File> filesToRemove);
        ICollection<File> ExtractFilesFromForm(ICollection<IFormFile> formFiles, int materialId);
        void SaveFilesToFileSystem(ICollection<IFormFile> formFiles, int materialId);
        string GetUploadFolderAbsolutePath();
        void SaveEditRequestFilesToFileSystem(ICollection<IFormFile> formFiles, int materialId, int editRequestId);
        void ReplaceMaterialFilesWithEditRequestFiles(int materialId, int editRequestId, ICollection<File> files);
    }
}
