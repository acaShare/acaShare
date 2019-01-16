using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models;
using acaShare.MVC.Common;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole + ", " + Roles.ModeratorRole)]
    [Area("Moderator")]
    public class ModeratorPanelController : Controller
    {
        private readonly IMaterialsService _materialsService;
        private readonly IUserService _userService;
        private readonly IFormFilesManagement _filesManagement;

        public ModeratorPanelController(IMaterialsService materialsService, IUserService userService, IFormFilesManagement formFilesManagement)
        {
            _materialsService = materialsService;
            _userService = userService;
            _filesManagement = formFilesManagement;
        }

        public IActionResult MaterialsToApprove()
        {
            ConfigureBreadcrumbs();

            var materialsToApprove = _materialsService.GetMaterialsToApprove();

            var vm = materialsToApprove.Select(m => 
                new MaterialToApproveViewModel
                {
                    MaterialId = m.MaterialId,
                    Name = m.Name,
                    Description = m.Description,
                    CreatorUsername = m.Creator.Username,
                    UploadDate = m.UploadDate
                }
            ).ToList();

            return View(vm);
        }

        private void ConfigureBreadcrumbs()
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "ModeratorPanel",
                    Action = "MaterialsToApprove",
                    Title = "Materiały oczekujące na zatwierdzenie"
                }
            };
        }

        public IActionResult MaterialApprovalDecision(int materialId)
        {
            var materialToApprove = _materialsService.GetMaterialToApprove(materialId);
            if (materialToApprove == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje." });
            }

            ConfigureMaterialBreadcrumbs(materialId);

            var vm = new MaterialToApproveViewModel
            {
                MaterialId = materialToApprove.MaterialId,
                Name = materialToApprove.Name,
                Description = materialToApprove.Description,
                CreatorUsername = materialToApprove.Creator.Username,
                UploadDate = materialToApprove.UploadDate,
                Files = materialToApprove.Files.Select(f => 
                    new FileViewModel
                    {
                        FileId = f.FileId,
                        FileName = f.FileName,
                        RelativePath = f.RelativePath,
                        ContentType = f.ContentType
                    }
                ).ToList()
            };

            return View(vm);
        }

        private void ConfigureMaterialBreadcrumbs(int materialId)
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "ModeratorPanel",
                    Action = "MaterialsToApprove",
                    Title = "Materiały oczekujące na zatwierdzenie"
                },
                new Breadcrumb
                {
                    Controller = "ModeratorPanel",
                    Action = "MaterialApprovalDecision",
                    Title = "Materiał",
                    Params = new Dictionary<string, string> { { "materialId", materialId.ToString() } }
                }
            };
        }

        public IActionResult ApproveMaterial(int materialId)
        {
            var loggedUser = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var material = _materialsService.GetMaterial(materialId);
            if (material == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje." });
            }

            _materialsService.ApproveMaterial(material, loggedUser);

            return RedirectToAction("MaterialsToApprove");
        }
        
        public IActionResult RejectMaterial(int materialId)
        {
            var materialToReject = _materialsService.GetMaterial(materialId);
            if (materialToReject == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje." });
            }

            _filesManagement.DeleteWholeMaterialFolder(materialId);
            _materialsService.RejectMaterial(materialToReject);
            return RedirectToAction("MaterialsToApprove");
        }
    }
}