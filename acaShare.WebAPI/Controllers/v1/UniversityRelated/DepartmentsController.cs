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
    public class DepartmentsController : ControllerBase
    {
        private readonly IUniversityTreeManagementService _service;

        public DepartmentsController(IUniversityTreeManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return _service.GetDepartments();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> Get(int id)
        {
            return _service.GetDepartment(id);
        }

        [HttpPost]
        public void Post(Department department)
        {
            _service.AddDepartment(department);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _service.DeleteDepartment(id);
        }

        [HttpPut]
        public void Update(Department department)
        {
            _service.UpdateDepartment(department);
        }
    }
}