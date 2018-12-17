using acaShare.BLL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Common
{
    public class FormFilesManagement : IFormFilesManagement
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FormFilesManagement(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void RemoveFilesFromFileSystem(ICollection<BLL.Models.File> filesToRemove)
        {
            foreach (var file in filesToRemove)
            {
                var path = Path.Combine(GetUploadFolderAbsolutePath(), file.RelativePath);
                System.IO.File.Delete(path);
            }
        }

        public ICollection<BLL.Models.File> ExtractFilesFromForm(ICollection<IFormFile> formFiles, int materialId)
        {
            ICollection<BLL.Models.File> newFiles = new List<BLL.Models.File>();

            if (formFiles?.Count > 0)
            {
                foreach (var formFile in formFiles)
                {
                    if (formFile.Length > 0)
                    {
                        var relativePath = Path.Combine(
                            Properties.Resources.MaterialFilesUploadFolderName,
                            materialId.ToString(),
                            formFile.FileName);

                        var file = new BLL.Models.File(Path.GetFileNameWithoutExtension(formFile.FileName), relativePath, formFile.ContentType);
                        newFiles.Add(file);
                    }
                }
            }

            return newFiles;
        }

        public void SaveFilesToFileSystem(ICollection<IFormFile> formFiles, int materialId)
        {
            if (formFiles?.Count > 0)
            {
                foreach (var formFile in formFiles)
                {
                    if (formFile.Length > 0)
                    {
                        var relativePath = Path.Combine(
                            Properties.Resources.MaterialFilesUploadFolderName,
                            materialId.ToString(),
                            formFile.FileName);

                        var fileAbsolutePath = Path.Combine(GetUploadFolderAbsolutePath(), relativePath);

                        Directory.CreateDirectory(Path.GetDirectoryName(fileAbsolutePath));

                        using (var stream = new FileStream(fileAbsolutePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }
                    }
                }
            }
        }

        public void SaveEditRequestFilesToFileSystem(ICollection<IFormFile> formFiles, int materialId, int editRequestId)
        {
            if (formFiles?.Count > 0)
            {
                foreach (var formFile in formFiles)
                {
                    if (formFile.Length > 0)
                    {
                        var relativePath = Path.Combine(
                            Properties.Resources.MaterialFilesUploadFolderName,
                            materialId.ToString(),
                            Properties.Resources.EditRequestFilesUploadFolderName,
                            editRequestId.ToString(),
                            formFile.FileName);

                        var fileAbsolutePath = Path.Combine(GetUploadFolderAbsolutePath(), relativePath);

                        Directory.CreateDirectory(Path.GetDirectoryName(fileAbsolutePath));

                        using (var stream = new FileStream(fileAbsolutePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }
                    }
                }
            }
        }

        public string GetUploadFolderAbsolutePath()
        {
            return Path.Combine(_hostingEnvironment.ContentRootPath, Properties.Resources.UploadsFolderName);
        }

        public void ReplaceMaterialFilesWithEditRequestFiles(int materialId, int editRequestId, ICollection<BLL.Models.File> allNewFiles)
        {
            var materialFolderPath = Path.Combine(
                Properties.Resources.MaterialFilesUploadFolderName,
                materialId.ToString());

            var materialFolderAbsolutePath = Path.Combine(GetUploadFolderAbsolutePath(), materialFolderPath);

            var existingFilesToStay = allNewFiles.Where(f => f.FileId > 0).ToList();
            RemoveExistingMaterialFilesExceptSupplied(materialFolderAbsolutePath, existingFilesToStay);

            var newFiles = allNewFiles.Where(f => f.FileId > 0).ToList();
            MoveNewFilesFromEditRequestToMaterial(materialFolderAbsolutePath, editRequestId, newFiles);
        }

        private void RemoveExistingMaterialFilesExceptSupplied(string materialFolderAbsolutePath, List<BLL.Models.File> filesToStay)
        {
            var existingFilesPathsInFileServer = Directory.GetFiles(materialFolderAbsolutePath);

            foreach (var existingFilePathInFileServer in existingFilesPathsInFileServer)
            {
                var existingFile = filesToStay.FirstOrDefault(ef => existingFilePathInFileServer.EndsWith(ef.RelativePath));

                if (existingFile == null)
                {
                    Directory.Delete(existingFilePathInFileServer);
                }
            }
        }
        
        private void MoveNewFilesFromEditRequestToMaterial(string materialFolderAbsolutePath, int editRequestId, List<BLL.Models.File> newFiles)
        {
            var pathToEditRequestFiles = Path.Combine(
                materialFolderAbsolutePath, 
                Properties.Resources.EditRequestFilesUploadFolderName,
                editRequestId.ToString());

            var editRequestFilesPaths = Directory.GetFiles(materialFolderAbsolutePath);

            foreach (var filePath in editRequestFilesPaths)
            {
                var destination = Path.Combine(materialFolderAbsolutePath, Path.GetFileName(filePath));
                System.IO.File.Move(filePath, destination);
            }

            Directory.Delete(pathToEditRequestFiles);
        }
    }
}
