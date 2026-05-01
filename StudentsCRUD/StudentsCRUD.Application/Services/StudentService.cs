using StudentsCRUD.Application.DTOs;
using StudentsCRUD.Application.Interfaces;
using StudentsCRUD.Domain.Entities;
using StudentsCRUD.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace StudentsCRUD.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<StudentDto> GetAllStudents()
        {
            return _repository.GetAll().Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Age = s.Age
            });
        }

        public StudentDto GetStudentById(int id)
        {
            var s = _repository.GetById(id);
            if (s == null) return null;
            return new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Age = s.Age
            };
        }

        public void CreateStudent(StudentDto dto)
        {
            _repository.Add(new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Age = dto.Age
            });
        }

        public void UpdateStudent(StudentDto dto)
        {
            _repository.Update(new Student
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Age = dto.Age
            });
        }

        public void DeleteStudent(int id)
        {
            _repository.Delete(id);
        }
    }
}