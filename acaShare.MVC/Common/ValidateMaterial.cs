using acaShare.MVC.Areas.Main.Models.Materials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Common
{
    public class ValidateMaterial : ActionFilterAttribute
    {
        public string MaterialViewModelParam { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionArguments = context.ActionArguments;

            if (actionArguments.ContainsKey(MaterialViewModelParam))
            {
                var modelState = context.ModelState;
                var vm = ((IMaterialManagementViewModel)actionArguments[MaterialViewModelParam]);

                if (!UploadedFilesAreValid(vm.FormFiles))
                {
                    int i = vm.StartingId;
                    foreach (var file in vm.FormFiles)
                    {
                        if (IsNotValidUploadFile(file))
                        {
                            string error = string.Empty;

                            if (!HasFileName(file))
                            {
                                error = $"Nazwa pliku numer {i + 1} jest wymagana";
                            }

                            if (!HasFileNameRequiredLength(file.FileName))
                            {
                                error = $"Nazwa pliku numer {i + 1} nie\nmoże przekraczać 50 znaków";
                            }

                            modelState.AddModelError($"FormFile[{i}]__FileName", error);
                        }

                        i++;
                    }
                }

                if (!modelState.IsValid)
                {
                    context.Result = new BadRequestObjectResult(modelState.Errors());
                }
            }
        }

        private bool UploadedFilesAreValid(ICollection<IFormFile> formFiles)
        {
            return !formFiles?.Any(f => IsNotValidUploadFile(f)) ?? true;
        }

        private bool IsNotValidUploadFile(IFormFile file)
        {
            return !HasFileName(file) || !HasFileNameRequiredLength(file.FileName);
        }

        private bool HasFileName(IFormFile file)
        {
            return !string.IsNullOrEmpty(file.FileName) && !string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(file.FileName));
        }

        private bool HasFileNameRequiredLength(string fileName)
        {
            return fileName.Length <= 50;
        }
    }
}
