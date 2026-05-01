using StudentsCRUD.Application.DTOs;
using System.Collections.Generic;

namespace StudentsCRUD.Application.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<StudentDto> GetAllStudents();
        StudentDto GetStudentById(int id);
        void CreateStudent(StudentDto dto);
        void UpdateStudent(StudentDto dto);
        void DeleteStudent(int id);
    }
}