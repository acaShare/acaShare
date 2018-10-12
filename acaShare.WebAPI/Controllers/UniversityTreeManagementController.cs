using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.BLL.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.WebAPI.Controllers
{
    // TODO: Move methods to separate controllers (controller per entity)
    [Route("[controller]/[action]")]
    [ApiController]
    public class UniversityTreeManagementController : ControllerBase
    {
        private readonly IUniversityTreeManagementService _moderatorPanelService;

        public UniversityTreeManagementController(IUniversityTreeManagementService moderatorPanelService)
        {
            _moderatorPanelService = moderatorPanelService;
        }

        [HttpPost]
        public void AddUniversity(University university)
        {
            _moderatorPanelService.AddUniversity(university);
        }

        [HttpPost]
        public void AddDepartment(Department department)
        {
            _moderatorPanelService.AddDepartment(department);
        }

        [HttpPost]
        public void AddLecturer(Lecturer lecturer)
        {
            _moderatorPanelService.AddLecturer(lecturer);
        }

        [HttpPost]
        public void AddLesson(Lesson lesson)
        {
            _moderatorPanelService.AddLesson(lesson);
        }

        [HttpPost]
        public void AddSectionOfSubject(SectionOfSubject sectionOfSubject)
        {
            _moderatorPanelService.AddSectionOfSubject(sectionOfSubject);
        }

        [HttpPost]
        public void AddSubject(Subject subject)
        {
            _moderatorPanelService.AddSubject(subject);
        }

        [HttpDelete]
        public void DeleteDepartment(int departmentId)
        {
            _moderatorPanelService.DeleteDepartment(departmentId);
        }

        [HttpDelete]
        public void DeleteLecturer(int lecturerId)
        {
            _moderatorPanelService.DeleteLecturer(lecturerId);
        }

        [HttpDelete]
        public void DeleteLesson(int lessonId)
        {
            _moderatorPanelService.DeleteLesson(lessonId);
        }

        [HttpDelete]
        public void DeleteSectionOfSubject(int sectionOfSubjectId)
        {
            _moderatorPanelService.DeleteSectionOfSubject(sectionOfSubjectId);
        }

        [HttpDelete]
        public void DeleteSubject(int subjectId)
        {
            _moderatorPanelService.DeleteSubject(subjectId);
        }

        [HttpDelete]
        public void DeleteUniversity(int universityId)
        {
            _moderatorPanelService.DeleteUniversity(universityId);
        }

        [HttpPut]
        public void UpdateDepartment(Department department)
        {
            _moderatorPanelService.UpdateDepartment(department);
        }

        [HttpPut]
        public void UpdateLecturer(Lecturer lecturer)
        {
            _moderatorPanelService.UpdateLecturer(lecturer);
        }

        [HttpPut]
        public void UpdateLesson(Lesson lesson)
        {
            _moderatorPanelService.UpdateLesson(lesson);
        }

        [HttpPut]
        public void UpdateSectionOfSubject(SectionOfSubject sectionOfSubject)
        {
            _moderatorPanelService.UpdateSectionOfSubject(sectionOfSubject);
        }

        [HttpPut]
        public void UpdateSubject(Subject subject)
        {
            _moderatorPanelService.UpdateSubject(subject);
        }

        [HttpPut]
        public void UpdateUniversity(University university)
        {
            _moderatorPanelService.UpdateUniversity(university);
        }
    }
}