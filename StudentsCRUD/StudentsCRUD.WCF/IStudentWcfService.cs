using StudentsCRUD.Application.DTOs;
using System.Collections.Generic;
using System.ServiceModel;

namespace StudentsCRUD.WCF
{
    [ServiceContract]
    public interface IStudentWcfService
    {
        [OperationContract]
        List<StudentDto> GetAllStudents();

        [OperationContract]
        StudentDto GetStudentById(int id);

        [OperationContract]
        void CreateStudent(StudentDto dto);

        [OperationContract]
        void UpdateStudent(StudentDto dto);

        [OperationContract]
        void DeleteStudent(int id);
    }
}