using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.BLL.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.WebAPI.Controllers.v1.UniversityRelated
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsOfSubjectController : ControllerBase
    {
        private readonly IUniversityTreeManagementService _service;

        public SectionsOfSubjectController(IUniversityTreeManagementService service)
        {
            _service = service;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<SectionOfSubject>> Get()
        //{
        //    return _service.GetSectionsOfSubject();
        //}

        //[HttpGet("{id}")]
        //public ActionResult<SectionOfSubject> Get(int id)
        //{
        //    return _service.GetSectionOfSubject(id);
        //}

        //[HttpPost]
        //public void Post(SectionOfSubject sectionOfSubject)
        //{
        //    _service.AddSectionOfSubject(sectionOfSubject);
        //}

        //[HttpDelete]
        //public void Delete(int id)
        //{
        //    _service.DeleteSectionOfSubject(id);
        //}

        //[HttpPut]
        //public void Update(SectionOfSubject sectionOfSubject)
        //{
        //    _service.UpdateSectionOfSubject(sectionOfSubject);
        //}
    }
}