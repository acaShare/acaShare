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
    public class SubjectsController : ControllerBase
    {
        private readonly IUniversityTreeManagementService _service;

        public SubjectsController(IUniversityTreeManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Subject>> Get()
        {
            return _service.GetSubjects();
        }

        [HttpGet("{id}")]
        public ActionResult<Subject> Get(int id)
        {
            return _service.GetSubject(id);
        }

        [HttpPost]
        public void Post(Subject subject)
        {
            _service.AddSubject(subject);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _service.DeleteSubject(id);
        }

        [HttpPut]
        public void Update(Subject subject)
        {
            _service.UpdateSubject(subject);
        }
    }
}