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

        [HttpGet]
        public IEnumerable<StudentDto> GetAll() =>
            _service.GetAllStudents();

        [HttpGet]
        public StudentDto Get(int id) =>
            _service.GetStudentById(id);

        [HttpPost]
        public void Post([FromBody] StudentDto dto) =>
            _service.CreateStudent(dto);

        [HttpPut]
        public void Put(int id, [FromBody] StudentDto dto)
        {
            dto.Id = id;
            _service.UpdateStudent(dto);
        }

        [HttpDelete]
        public void Delete(int id) =>
            _service.DeleteStudent(id);
    }
}