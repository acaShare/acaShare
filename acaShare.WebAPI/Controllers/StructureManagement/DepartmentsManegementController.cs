using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Common;
using acaShare.WebAPI.Models;
using acaShare.WebAPI.Models.StructureTraversal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace acaShare.WebAPI.Controllers.StructureManagement
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole)]
    [ApiController]
    [Route("api/v1/universities/{universityId:int}/departments")]
    public class DepartmentsManagementController : ControllerBase
    {
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;
        private readonly IMaterialsService _materialsService;
        private readonly IFormFilesManagement _filesManagement;

        public DepartmentsManagementController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService,
            IMaterialsService materialsService, IFormFilesManagement formFilesManagement)
        {
            _traversalService = traversalService;
            _managementService = managementService;
            _materialsService = materialsService;
            _filesManagement = formFilesManagement;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DepartmentViewModel>> Get(int universityId)
        {
            var university = _traversalService.GetUniversity(universityId);
            if (university == null)
            {
                return NotFound("Uczelnia o takim id nie istnieje.");
            }

            var departments = _traversalService.GetDepartmentsFromUniversity(universityId);

            return departments.Select(d =>
                new DepartmentViewModel
                {
                    Id = d.DepartmentId,
                    Name = d.Name,
                    Abbreviation = d.Abbreviation
                }
            ).ToList();
        }


        [HttpGet("{departmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DepartmentViewModel> Get(int departmentId, string fakeParam = null)
        {
            var department = _traversalService.GetDepartment(departmentId);

            if (department == null)
            {
                return NotFound();
            }

            return new DepartmentViewModel
            {
                Id = department.DepartmentId,
                Name = department.Name,
                Abbreviation = department.Abbreviation
            };
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post(int universityId, DepartmentViewModel vm)
        {
            var university = _traversalService.GetUniversity(universityId);
            if (university == null)
            {
                return NotFound("Uczelnia o takim id nie istnieje.");
            }

            var departmentToAdd = new BLL.Models.Department(vm.Name, vm.Abbreviation, university);

            var success = _managementService.AddDepartment(departmentToAdd);

            if (!success)
            {
                return Conflict("Wydział o takiej nazwie lub skrócie istnieje już na tej uczelni");
            }

            return NoContent();
        }


        [HttpPut("{departmentId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Edit(int departmentId, DepartmentViewModel vm)
        {
            var departmentToEdit = _traversalService.GetDepartment(vm.Id);
            if (departmentToEdit == null)
            {
                return NotFound("Wydział o takim id nie istnieje.");
            }

            departmentToEdit.Update(vm.Name, vm.Abbreviation);

            bool success = _managementService.UpdateDepartment(departmentToEdit);

            if (!success)
            {
                return Conflict("Wydział o takiej nazwie lub skrócie istnieje już na tej uczelni");
            }

            return NoContent();
        }


        [HttpDelete("/api/v1/departments/{departmentId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int departmentId)
        {
            var departmentToDelete = _traversalService.GetDepartment(departmentId);
            if (departmentToDelete == null)
            {
                return NotFound("Wydział o takim id nie istnieje.");
            }

            // First - delete materials due to database constraints betwee Lesson and Material
            foreach (var lesson in departmentToDelete.Lessons)
            {
                foreach (var materialToDelete in lesson.Materials)
                {
                    _filesManagement.DeleteWholeMaterialFolder(materialToDelete.MaterialId);
                    _materialsService.DeleteMaterial(materialToDelete);
                }
            }

            // actually delete
            _managementService.DeleteDepartment(departmentToDelete);

            return NoContent();
        }
    }
}