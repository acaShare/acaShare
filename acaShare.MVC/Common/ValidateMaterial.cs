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
        public string ViewModelToValidateParam { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionArguments = context.ActionArguments;

            if (actionArguments.ContainsKey(ViewModelToValidateParam))
            {
                var modelState = context.ModelState;
                var vm = ((IMaterialManagementViewModel)actionArguments[ViewModelToValidateParam]);

                if (!FormFilesValidator.AreUploadedFilesValid(vm.FormFiles, maxFileNameLength: 50))
                {
                    int i = vm.StartingId;
                    foreach (var file in vm.FormFiles)
                    {
                        if (FormFilesValidator.IsNotValidUploadFile(file))
                        {
                            string error = string.Empty;

                            if (!FormFilesValidator.HasFileName(file))
                            {
                                error = $"Nazwa pliku numer {i + 1} jest wymagana";
                            }

                            if (!FormFilesValidator.HasFileNameRequiredLength(file.FileName))
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
    }
}
