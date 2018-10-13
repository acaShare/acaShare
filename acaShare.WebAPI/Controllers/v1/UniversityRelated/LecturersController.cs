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
    public class LecturersController : ControllerBase
    {
        private readonly IUniversityTreeManagementService _service;

        public LecturersController(IUniversityTreeManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Lecturer>> Get()
        {
            return _service.GetLecturers();
        }

        [HttpGet("{id}")]
        public ActionResult<Lecturer> Get(int id)
        {
            return _service.GetLecturer(id);
        }

        [HttpPost]
        public void Post(Lecturer lecturer)
        {
            _service.AddLecturer(lecturer);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _service.DeleteLecturer(id);
        }

        [HttpPut]
        public void Update(Lecturer lecturer)
        {
            _service.UpdateLecturer(lecturer);
        }
    }
}