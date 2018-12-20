using acaShare.MVC.Areas.Main.Models.Materials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static acaShare.MVC.Areas.Main.Controllers.MaterialsController;

namespace acaShare.MVC.Common
{
    public class ValidateMaterial : ActionFilterAttribute
    {
        public string ViewModelName { get; set; }

        private IFilesValidator _filesValidator;
        private static readonly int MaxFileNameLength = 100;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _filesValidator = (IFilesValidator)context.HttpContext.RequestServices.GetService(typeof(IFilesValidator));
            _filesValidator.MaxFileNameLength = MaxFileNameLength;

            var actionArguments = context.ActionArguments;

            if (actionArguments.ContainsKey(ViewModelName))
            {
                var modelState = context.ModelState;
                var vm = ((IMaterialManagementViewModel)actionArguments[ViewModelName]);

                ValidateFormFiles(modelState, vm.FormFiles, vm.StartingId);
                ValidateFileNames(modelState, actionArguments);

                if (!modelState.IsValid)
                {
                    context.Result = new BadRequestObjectResult(modelState.Errors());
                }
            }
        }

        private void ValidateFormFiles(ModelStateDictionary modelState, ICollection<IFormFile> formFiles, int startingId)
        {
            if (!_filesValidator.AreUploadedFilesValid(formFiles))
            {
                int i = startingId;
                foreach (var file in formFiles)
                {
                    if (_filesValidator.IsNotValidUploadFile(file))
                    {
                        string error = string.Empty;

                        if (!_filesValidator.HasFileName(file))
                        {
                            error = $"Nazwa pliku numer {i + 1} jest wymagana";
                        }

                        if (!_filesValidator.HasFileNameRequiredLength(file.FileName))
                        {
                            error = $"Nazwa pliku numer {i + 1} nie\nmoże przekraczać {MaxFileNameLength} znaków";
                        }

                        modelState.AddModelError($"FormFile[{i}]__FileName", error);
                    }

                    i++;
                }
            }
        }

        private void ValidateFileNames(ModelStateDictionary modelState, IDictionary<string, object> actionArguments)
        {
            if (actionArguments[ViewModelName] is IEditViewModel vm)
            {
                var repeatedFileNames = _filesValidator.ValidateFileNames(vm.FormFiles, vm.Files);

                if (repeatedFileNames.Count > 0)
                {
                    int i = 0;
                    foreach (var repeatedFileName in repeatedFileNames.Distinct())
                    {
                        modelState.AddModelError($"Files[{i++}]_FileName", $"Plik o nazwie \"{repeatedFileName}\" już istnieje");
                    }
                }
            }
        }

    }
}
