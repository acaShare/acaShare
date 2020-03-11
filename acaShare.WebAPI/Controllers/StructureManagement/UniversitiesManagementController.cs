using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Common;
using acaShare.WebAPI.Models;
using acaShare.WebAPI.Models.StructureTraversal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace acaShare.WebAPI.Controllers.StructureManagement
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UniversitiesManagementController : Controller
    {
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;
        private readonly IMaterialsService _materialsService;
        private readonly IFormFilesManagement _filesManagement;
        private readonly IUserService _userservice;
        private readonly IMainModeratorService _mainModeratorService;

        public UniversitiesManagementController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService,
            IMaterialsService materialsService, IFormFilesManagement formFilesManagement, IUserService userService, IMainModeratorService mainModeratorService)
        {
            _traversalService = traversalService;
            _managementService = managementService;
            _materialsService = materialsService;
            _filesManagement = formFilesManagement;
            _userservice = userService;
            _mainModeratorService = mainModeratorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<UniversityViewModel> Get()
        {
            if (User.IsInRole(Roles.MainModeratorRole))
            {
                var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var appUser = _userservice.FindByIdentityUserId(identityUserId);
                var mainModeratorUniversity = _mainModeratorService.GetUniversityMainModerator(appUser.UserId);
                var university = _traversalService.GetUniversity(mainModeratorUniversity.UniversityId);

                return new List<UniversityViewModel>
                {
                    new UniversityViewModel
                    {
                        Id = university.UniversityId,
                        TitleOrFullName = university.Name,
                        SubtitleOrAbbreviation = university.Abbreviation
                    }
                };
            }
            else
            {
                var universities = _traversalService.GetUniversities();

                return universities.Select(u =>
                    new UniversityViewModel
                    {
                        Id = u.UniversityId,
                        TitleOrFullName = u.Name,
                        SubtitleOrAbbreviation = u.Abbreviation,
                    }
                ).ToList();
            }
        }

        [Authorize(Roles = Roles.AdministratorRole)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public IActionResult Post(UniversityViewModel vm)
        {
            var universityToAdd = new BLL.Models.University(vm.TitleOrFullName, vm.SubtitleOrAbbreviation);

            var success = _managementService.AddUniversity(universityToAdd);

            if (!success)
            {
                return Conflict("Uczelnia o takiej nazwie lub skrócie już istnieje");
            }

            return CreatedAtAction(nameof(Get), universityToAdd);
        }

        [Authorize(Roles = Roles.AdministratorRole)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut]
        public IActionResult Put(UniversityViewModel vm)
        {
            var universityToEdit = _traversalService.GetUniversity(vm.Id);
            if (universityToEdit == null)
            {
                return NotFound();
            }

            universityToEdit.Update(vm.TitleOrFullName, vm.SubtitleOrAbbreviation);

            bool success = _managementService.UpdateUniversity(universityToEdit);

            if (!success)
            {
                return Conflict("Uczelnia o takiej nazwie lub skrócie już istnieje");
            }

            return NoContent();
        }

        [Authorize(Roles = Roles.AdministratorRole)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{universityId}")]
        public IActionResult Delete(int universityId)
        {
            var universityToDelete = _traversalService.GetUniversity(universityId);
            if (universityToDelete == null)
            {
                return NotFound();
            }
            
            // First - delete materials due to database constraints betwee Lesson and Material
            foreach (var dept in universityToDelete.Departments)
            {
                foreach (var lesson in dept.Lessons)
                {
                    foreach (var materialToDelete in lesson.Materials)
                    {
                        _filesManagement.DeleteWholeMaterialFolder(materialToDelete.MaterialId);
                        _materialsService.DeleteMaterial(materialToDelete);
                    }
                }
            }

            // actually delete
            _managementService.DeleteUniversity(universityToDelete);

            return NoContent();
        }
    }
}