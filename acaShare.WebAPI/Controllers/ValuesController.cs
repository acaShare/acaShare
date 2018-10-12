using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.BLL.Models;
using acaShare.DAL.Core;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public ValuesController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<dynamic>> Get()
        {
            return uow.Lessons.GetAll().Select(l => new {
                SemesterNumber = l.Semester.SemesterNumber.Number,
                YearFrom = l.Semester.AcademicYear.YearFrom.Year,
                YearTo = l.Semester.AcademicYear.YearTo.Year,
                LecturerName = l.Lecturer.Name,
                Department = l.Department.Name,
                University = l.Department.University.Name
            }).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var lesson = uow.Lessons.FindById(id);
            return lesson == null ? "Nie ma takiego lesson" : lesson.SectionOfSubject.Subject.Name;
        }

        // POST api/values
        [HttpPost]
        public void Post(string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
