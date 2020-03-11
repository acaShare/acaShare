using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Common;
using acaShare.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace acaShare.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MaterialsToApproveController : ControllerBase
    {
        private readonly IMaterialsService _materialsService;
        private readonly IUserService _userService;
        private readonly IFormFilesManagement _filesManagement;

        public MaterialsToApproveController(IMaterialsService materialsService, IUserService userService, IFormFilesManagement formFilesManagement)
        {
            _materialsService = materialsService;
            _userService = userService;
            _filesManagement = formFilesManagement;
        }

        [HttpGet]
        public IEnumerable<MaterialToApproveViewModel> Get()
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

            return vm;
        }

        [HttpGet("{materialId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MaterialToApproveViewModel> Get(int materialId)
        {
            var materialToApprove = _materialsService.GetMaterialToApprove(materialId);
            if (materialToApprove == null)
            {
                return NotFound();
            }

            return new MaterialToApproveViewModel
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
        }

        [HttpPost]
        public IActionResult Update(MaterialToApproveDto dto)
        {
            var loggedUser = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var material = _materialsService.GetMaterial(dto.MaterialId);
            if (material == null)
            {
                return NotFound();
            }

            if (dto.ShouldApprove)
            {
                _materialsService.ApproveMaterial(material, loggedUser);
            }
            else
            {
                _filesManagement.DeleteWholeMaterialFolder(dto.MaterialId);
                _materialsService.RejectMaterial(material);
            }

            return NoContent();
        }
    }
}