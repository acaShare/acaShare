using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace acaShare.MVC.Common
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<string> Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.Values
                    .SelectMany(m => m.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
            }

            return Enumerable.Empty<string>();
        }
    }
}
