using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public interface IEditViewModel
    {
        ICollection<FileViewModel> Files { get; set; }
        ICollection<IFormFile> FormFiles { get; set; }
    }
}
