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
    public class DepartmentsOfUniversityController : ControllerBase
    {
        private readonly IUniversityTreeTraversalService _service;

        public DepartmentsOfUniversityController(IUniversityTreeTraversalService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Department>> Get(int id)
        {
            return _service.GetUniversity(id).Departments.ToList();
        }
    }



    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;

        public DepartmentsController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService)
        {
            _traversalService = traversalService;
            _managementService = managementService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return _traversalService.GetDepartmentsFromUniversity(-1).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> Get(int id)
        {
            return _traversalService.GetDepartment(id);
        }

        [HttpPost]
        public void Post(Department department)
        {
            _managementService.AddDepartment(department);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var departmentToDelete = _traversalService.GetDepartment(id);
            _managementService.DeleteDepartment(departmentToDelete);
        }

        [HttpPut]
        public void Update(Department department)
        {
            _managementService.UpdateDepartment(department);
        }
    }
}