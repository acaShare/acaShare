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
    [Route("api/v1/[controller]")]
    public class LessonsManagementController : ControllerBase
    {
        /// <summary>
        /// The controller's CUD methods manage subjects in department, but show Lessons. It is a side effect of our db archritecture.
        /// In fact, Lesson IS SubjectDepartment in a given semester
        /// </summary>
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;
        private readonly IMaterialsService _materialsService;
        private readonly IFormFilesManagement _filesManagement;

        public LessonsManagementController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService,
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
        public ActionResult<IEnumerable<LessonViewModel>> Get(int semesterId, int departmentId)
        {
            var semester = _traversalService.GetSemester(semesterId);
            if (semester == null)
            {
                return NotFound("Semestr o takim Id nie istnieje.");
            }

            var department = _traversalService.GetDepartment(departmentId);
            if (department == null)
            {
                return NotFound("Wydział o takim Id nie istnieje");
            }

            var lessons = _traversalService.GetLessons(semester, department);

            return lessons.Select(l =>
                new LessonViewModel
                {
                    Id = l.LessonId,
                    TitleOrFullName = l.Subject.Name,
                    SubtitleOrAbbreviation = l.Subject.Abbreviation
                }
            ).ToList();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Post(SubjectDepartmentViewModel vm)
        {
            var semester = _traversalService.GetSemester(vm.SemesterId);
            if (semester == null)
            {
                return NotFound("Semestr o takim Id nie istnieje.");
            }

            var department = _traversalService.GetDepartment(vm.DepartmentId);
            if (department == null)
            {
                return NotFound("Wydział o takim Id nie istnieje");
            }

            var lesson = new BLL.Models.Lesson(vm.SemesterId, department, vm.TitleOrFullName, vm.SubtitleOrAbbreviation);
            var success = _managementService.AddLesson(lesson);

            if (!success)
            {
                return Conflict("Przedmiot o takiej nazwie lub skrócie istnieje już na tym wydziale w tym semestrze");
            }

            return NoContent();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Put(LessonViewModel vm)
        {
            var lessonToEdit = _traversalService.GetLesson(vm.Id);
            if (lessonToEdit == null)
            {
                return NotFound("Przedmiot o takim Id nie istnieje.");
            }

            bool success = _managementService.UpdateLesson(lessonToEdit.LessonId, vm.TitleOrFullName, vm.SubtitleOrAbbreviation);

            if (!success)
            {
                return Conflict("Przedmiot o takiej nazwie lub skrócie istnieje już na tym wydziale w tym semestrze");
            }

            return NoContent();
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int lessonId)
        {
            var lessonToDelete = _traversalService.GetLesson(lessonId);
            if (lessonToDelete == null)
            {
                return NotFound("Przedmiot o takim Id nie istnieje.");
            }

            // First - delete materials due to database constraints betwee Lesson and Material
            foreach (var material in lessonToDelete.Materials)
            {
                _filesManagement.DeleteWholeMaterialFolder(material.MaterialId);
                _materialsService.DeleteMaterial(material);
            }

            // actually delete
            _managementService.DeleteLesson(lessonToDelete.LessonId);

            return NoContent();
        }
    }
}