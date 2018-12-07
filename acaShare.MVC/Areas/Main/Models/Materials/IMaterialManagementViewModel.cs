using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public interface IMaterialManagementViewModel
    {
        ICollection<IFormFile> FormFiles { get; set; }
        // used in ValidateMaterial ActionFilterAttribute to give proper information about which files are not valid
        int StartingId { get; set; }
    }
}
