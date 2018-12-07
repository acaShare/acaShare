﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers
{
    [Authorize]
    [Area("Moderator")]
    public class ModeratorPanelController : Controller
    {
        private readonly IUniversityTreeTraversalService _treeManagementservice;
        private readonly IMaterialsService _materialsService;
        private readonly IUserService _userService;

        public ModeratorPanelController(IUniversityTreeTraversalService treeManagementService, IMaterialsService materialsService, IUserService userService)
        {
            _treeManagementservice = treeManagementService;
            _materialsService = materialsService;
            _userService = userService;
        }

        public IActionResult Home()
        {
            ViewBag.IsRoot = true;
            return View();
        }

        public IActionResult MaterialsToApprove()
        {
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

        public IActionResult MaterialApprovalDecision(int materialId)
        {
            var materialToApprove = _materialsService.GetMaterialToApprove(materialId);

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
        
        public IActionResult ApproveMaterial(int materialId)
        {
            var loggedUser = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _materialsService.ApproveMaterial(materialId, loggedUser);

            return RedirectToAction("MaterialsToApprove");
        }
        
        public IActionResult RejectMaterial(int materialId)
        {
            _materialsService.RejectMaterial(materialId);
            return RedirectToAction("MaterialsToApprove");
        }
    }
}