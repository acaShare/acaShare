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
        private readonly IUniversityTreeManagementService _service;

        public LessonsFromDepartmentController(IUniversityTreeManagementService service)
        {
            _service = service;
        }
        
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Lesson>> Get(int id)
        {
            return _service.GetDepartment(id).Lessons.ToList();
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
        private readonly IUniversityTreeManagementService _service;

        public LessonsController(IUniversityTreeManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Lesson>> Get()
        {
            return _service.GetLessons();
        }

        [HttpGet("{id}")]
        public ActionResult<Lesson> Get(int id)
        {
            return _service.GetLesson(id);
        }

        [HttpPost]
        public void Post(Lesson lesson)
        {
            _service.AddLesson(lesson);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _service.DeleteLesson(id);
        }

        [HttpPut]
        public void Update(Lesson lesson)
        {
            _service.UpdateLesson(lesson);
        }
    }
}