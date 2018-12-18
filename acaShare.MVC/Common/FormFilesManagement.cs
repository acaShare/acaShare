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

        public ICollection<BLL.Models.File> ExtractFilesFromForm(ICollection<IFormFile> formFiles, int materialId, int? editRequestId = null)
        {
            if (materialId < 1)
            {
                throw new ArgumentOutOfRangeException("Provided materialId is not valid");
            }

            ICollection<BLL.Models.File> newFiles = new List<BLL.Models.File>();

            if (formFiles?.Count > 0)
            {
                foreach (var formFile in formFiles)
                {
                    if (formFile.Length > 0)
                    {
                        var relativePath = string.Empty;

                        if (editRequestId == null)
                        {
                            relativePath = Path.Combine(
                                Properties.Resources.MaterialFilesUploadFolderName,
                                materialId.ToString(),
                                formFile.FileName);
                        }
                        else
                        {
                            if (editRequestId.Value < 1)
                            {
                                throw new ArgumentOutOfRangeException("Provided EditRequestId is not valid");
                            }
                            
                            relativePath = Path.Combine(
                                Properties.Resources.MaterialFilesUploadFolderName,
                                materialId.ToString(),
                                Properties.Resources.EditRequestFilesUploadFolderName,
                                editRequestId.ToString(),
                                formFile.FileName);
                        }

                        var file = new BLL.Models.File(Path.GetFileNameWithoutExtension(formFile.FileName), relativePath, formFile.ContentType);
                        newFiles.Add(file);
                    }
                }
            }

            return newFiles;
        }

        public void SaveFilesToFileSystem(ICollection<IFormFile> formFiles, int materialId, int? editRequestId = null)
        {
            if (materialId < 1)
            {
                throw new ArgumentOutOfRangeException("Provided materialId is not valid");
            }

            if (formFiles?.Count > 0)
            {
                foreach (var formFile in formFiles)
                {
                    var relativePath = string.Empty;

                    if (formFile.Length > 0)
                    {
                        if (editRequestId == null)
                        {
                            relativePath = Path.Combine(
                                Properties.Resources.MaterialFilesUploadFolderName,
                                materialId.ToString(),
                                formFile.FileName);
                        }
                        else
                        {
                            if (editRequestId.Value < 1)
                            {
                                throw new ArgumentOutOfRangeException("Provided EditRequestId is not valid");
                            }

                            relativePath = Path.Combine(
                                Properties.Resources.MaterialFilesUploadFolderName,
                                materialId.ToString(),
                                Properties.Resources.EditRequestFilesUploadFolderName,
                                editRequestId.ToString(),
                                formFile.FileName);
                        }

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
            if (materialId < 1 || editRequestId < 1)
            {
                throw new ArgumentOutOfRangeException("Provided materialId or editRequestId is not valid");
            }

            if (allNewFiles?.Count > 0)
            {
                var materialFolderAbsolutePath = Path.Combine(
                    GetUploadFolderAbsolutePath(),
                    Properties.Resources.MaterialFilesUploadFolderName,
                    materialId.ToString());

                RemoveOldFiles(materialFolderAbsolutePath, allNewFiles);

                MoveNewFilesFromEditRequestToMaterial(materialFolderAbsolutePath, editRequestId);
            }
        }

        private void RemoveOldFiles(string materialFolderAbsolutePath, ICollection<BLL.Models.File> newFiles)
        {
            var existingFilesPaths = Directory.GetFiles(materialFolderAbsolutePath);

            foreach (var existingFilePath in existingFilesPaths)
            {
                var existingFile = newFiles.FirstOrDefault(ef => existingFilePath.EndsWith(ef.RelativePath));

                if (existingFile == null)
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }
        }
        
        private void MoveNewFilesFromEditRequestToMaterial(string materialFolderAbsolutePath, int editRequestId)
        {
            var editRequestFolderAbsolutePath = Path.Combine(
                materialFolderAbsolutePath, 
                Properties.Resources.EditRequestFilesUploadFolderName,
                editRequestId.ToString());

            if(Directory.Exists(editRequestFolderAbsolutePath))
            {
                var editRequestFilesPaths = Directory.GetFiles(editRequestFolderAbsolutePath);

                foreach (var filePath in editRequestFilesPaths)
                {
                    var destination = Path.Combine(materialFolderAbsolutePath, Path.GetFileName(filePath));
                    System.IO.File.Move(filePath, destination);
                }

                Directory.Delete(editRequestFolderAbsolutePath);
            }
        }
    }
}
