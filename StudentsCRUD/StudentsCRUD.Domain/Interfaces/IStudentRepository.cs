using StudentsCRUD.Domain.Entities;
using System.Collections.Generic;

namespace StudentsCRUD.Domain.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student GetById(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(int id);
    }
}