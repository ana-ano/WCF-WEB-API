using StudentsCRUD.Application.DTOs;
using StudentsCRUD.Application.Interfaces;
using StudentsCRUD.Application.Services;
using StudentsCRUD.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace StudentsCRUD.API.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly IStudentService _service;

        public StudentsController()
        {
            var connStr = System.Configuration.ConfigurationManager
                .ConnectionStrings["StudentsDb"].ConnectionString;
            var repo = new StudentRepository(connStr);
            _service = new StudentService(repo);
        }

        // GET api/students
        [HttpGet]
        public IEnumerable<StudentDto> GetAll() =>
            _service.GetAllStudents();

        // GET api/students/1
        [HttpGet]
        public StudentDto Get(int id) =>
            _service.GetStudentById(id);

        // POST api/students
        [HttpPost]
        public void Post([FromBody] StudentDto dto) =>
            _service.CreateStudent(dto);

        // PUT api/students/1
        [HttpPut]
        public void Put(int id, [FromBody] StudentDto dto)
        {
            dto.Id = id;
            _service.UpdateStudent(dto);
        }

        // DELETE api/students/1
        [HttpDelete]
        public void Delete(int id) =>
            _service.DeleteStudent(id);
    }
}