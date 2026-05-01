using StudentsCRUD.Domain.Entities;
using StudentsCRUD.Domain.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StudentsCRUD.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Student> GetAll()
        {
            var list = new List<Student>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Students", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(Map(reader));
                }
            }
            return list;
        }

        public Student GetById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Students WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read()) return Map(reader);
                return null;
            }
        }

        public void Add(Student s)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Students (FirstName,LastName,Email,Age) VALUES (@fn,@ln,@em,@ag)", conn);
                cmd.Parameters.AddWithValue("@fn", s.FirstName);
                cmd.Parameters.AddWithValue("@ln", s.LastName);
                cmd.Parameters.AddWithValue("@em", s.Email);
                cmd.Parameters.AddWithValue("@ag", s.Age);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Student s)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Students SET FirstName=@fn,LastName=@ln,Email=@em,Age=@ag WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@fn", s.FirstName);
                cmd.Parameters.AddWithValue("@ln", s.LastName);
                cmd.Parameters.AddWithValue("@em", s.Email);
                cmd.Parameters.AddWithValue("@ag", s.Age);
                cmd.Parameters.AddWithValue("@id", s.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Students WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private Student Map(SqlDataReader r) => new Student
        {
            Id = (int)r["Id"],
            FirstName = r["FirstName"].ToString(),
            LastName = r["LastName"].ToString(),
            Email = r["Email"].ToString(),
            Age = (int)r["Age"]
        };
    }
}