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
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;

        public SubjectsController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService)
        {
            _traversalService = traversalService;
            _managementService = managementService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Subject>> Get()
        {
            return _traversalService.GetSubjects();
        }

        [HttpGet("{id}")]
        public ActionResult<Subject> Get(int id)
        {
            return _traversalService.GetSubject(id);
        }

        [HttpPost]
        public void Post(Subject subject)
        {
            _managementService.AddSubject(subject);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _managementService.DeleteSubject(id);
        }

        [HttpPut]
        public void Update(Subject subject)
        {
            _managementService.UpdateSubject(subject);
        }
    }
}