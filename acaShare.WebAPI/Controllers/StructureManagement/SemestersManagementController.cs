using acaShare.ServiceLayer.Interfaces;
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
    [Route("api/v1/[controller]")]
    public class SemestersManagementController : ControllerBase
    {
        private readonly IUniversityTreeTraversalService _traversalService;

        public SemestersManagementController(IUniversityTreeTraversalService traversalService)
        {
            _traversalService = traversalService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SemesterViewModel>> Get(int departmentId)
        {
            var department = _traversalService.GetDepartment(departmentId);
            if (department == null)
            {
                return NotFound("Wydział o takim id nie istnieje.");
            }

            var semesters = _traversalService.GetSemesters();

            return semesters
                .Select(s =>
                    new SemesterViewModel
                    {
                        Id = s.SemesterId,
                        Name = s.Number
                    })
                .OrderBy(s => s.Id)
                .ToList();
        }
    }
}