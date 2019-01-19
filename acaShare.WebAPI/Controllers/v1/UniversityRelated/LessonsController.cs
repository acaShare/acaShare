using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.BLL.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.WebAPI.Controllers.v1.UniversityRelated
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LessonsFromDepartmentController : ControllerBase
    {
        private readonly IUniversityTreeTraversalService _service;

        public LessonsFromDepartmentController(IUniversityTreeTraversalService service)
        {
            _service = service;
        }
        
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Lesson>> Get(int id)
        {
            return null;// _service.GetDepartment(id).Lessons.ToList();
        }

        [HttpGet("{id}/ooo/{pp}")]
        public ActionResult<string> xd(int id, int pp)
        {
            return (id+" "+pp);
        }
    }

    [Route("api/v1/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;

        public LessonsController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService)
        {
            _traversalService = traversalService;
            _managementService = managementService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Lesson>> Get()
        {
            return null;// _traversalService.GetLessons().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Lesson> Get(int id)
        {
            return _traversalService.GetLesson(id);
        }

        [HttpPost]
        public void Post(Lesson lesson)
        {
            //_managementService.AddLesson(lesson);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _managementService.DeleteLesson(id);
        }

        [HttpPut]
        public void Update(Lesson lesson)
        {
            _managementService.UpdateLesson(lesson);
        }
    }
}