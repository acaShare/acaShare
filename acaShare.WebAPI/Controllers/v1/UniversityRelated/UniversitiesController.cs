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
        private readonly IUniversityTreeManagementService _service;

        public UniversitiesController(IUniversityTreeManagementService moderatorPanelService)
        {
            _service = moderatorPanelService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<University>> Get()
        {
            return _service.GetUniversities();
        }

        [HttpGet("{id}")]
        public University Get(int id)
        {
            return _service.GetUniversity(id);
        }

        [HttpPost]
        public void Add(University university)
        {
            _service.AddUniversity(university);
        }
        
        [HttpDelete]
        public void Delete(int universityId)
        {
            _service.DeleteUniversity(universityId);
        }

        [HttpPut]
        public void Update(University university)
        {
            _service.UpdateUniversity(university);
        }
    }
}