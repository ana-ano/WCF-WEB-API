using Microsoft.AspNetCore.Mvc;
using StudentsCRUD.Application.DTOs;
using StudentsCRUD.Application.Interfaces;
using StudentsCRUD.Application.Services;
using StudentsCRUD.Infrastructure.Repositories;

namespace StudentsCRUD.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController()
        {
            var connStr = "Data Source=DESKTOP-SE1JF07\\SQLEXPRESS;Initial Catalog=StudentsDB;Integrated Security=True";
            var repo = new StudentRepository(connStr);
            _service = new StudentService(repo);
        }

        [HttpGet]
        public IActionResult GetAll() =>
            Ok(_service.GetAllStudents());

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            Ok(_service.GetStudentById(id));

        [HttpPost]
        public IActionResult Post([FromBody] StudentDto dto)
        {
            _service.CreateStudent(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StudentDto dto)
        {
            dto.Id = id;
            _service.UpdateStudent(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.DeleteStudent(id);
            return Ok();
        }
    }
}