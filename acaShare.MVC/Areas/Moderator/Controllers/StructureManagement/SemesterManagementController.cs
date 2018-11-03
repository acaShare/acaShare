using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models.StructureManagement;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers.StructureManagement
{
    [Authorize]
    [Area("Moderator")]
    public class SemesterManagementController : Controller
    {
       
    }
}