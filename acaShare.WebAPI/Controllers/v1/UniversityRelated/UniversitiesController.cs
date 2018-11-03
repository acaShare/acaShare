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
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;

        public UniversitiesController(IUniversityTreeManagementService managementService, IUniversityTreeTraversalService traversalService)
        {
            _managementService = managementService;
            _traversalService = traversalService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<University>> Get()
        {
            return _traversalService.GetUniversities().ToList();
        }

        [HttpGet("{id}")]
        public University Get(int id)
        {
            return _traversalService.GetUniversity(id);
        }

        [HttpPost]
        public void Add(University university)
        {
            _managementService.AddUniversity(university);
        }
        
        [HttpDelete]
        public void Delete(int universityId)
        {
            var universityToDelete = _traversalService.GetUniversity(universityId);
            _managementService.DeleteUniversity(universityToDelete);
        }

        [HttpPut]
        public void Update(University university)
        {
            _managementService.UpdateUniversity(university);
        }
    }
}