using StudentsCRUD.Application.DTOs;
using StudentsCRUD.Application.Interfaces;
using StudentsCRUD.Application.Services;
using StudentsCRUD.Domain.Interfaces;
using StudentsCRUD.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace StudentsCRUD.WCF
{
    public class StudentWcfService : IStudentWcfService
    {
        private readonly IStudentService _service;

        public StudentWcfService()
        {
            // Manual DI (WCF-ში IoC Container-ის გარეშე)
            var connStr = System.Configuration.ConfigurationManager
                .ConnectionStrings["StudentsDb"].ConnectionString;
            var repo = new StudentRepository(connStr);
            _service = new StudentService(repo);
        }

        public List<StudentDto> GetAllStudents() =>
            _service.GetAllStudents().ToList();

        public StudentDto GetStudentById(int id) =>
            _service.GetStudentById(id);

        public void CreateStudent(StudentDto dto) =>
            _service.CreateStudent(dto);

        public void UpdateStudent(StudentDto dto) =>
            _service.UpdateStudent(dto);

        public void DeleteStudent(int id) =>
            _service.DeleteStudent(id);
    }
}