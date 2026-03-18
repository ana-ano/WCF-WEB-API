using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WindowsFormsApp1;

public class DbHelper
{
    string connectionString = @"Data Source=.;Initial Catalog=StudentDB;Integrated Security=True";

    public List<Student> GetAll()
    {
        var list = new List<Student>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Students", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Student
                {
                    Id = (int)reader["Id"],
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Age = (int)reader["Age"]
                });
            }
        }
        return list;
    }

    public void Add(Student student)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Students (FirstName, LastName, Age) VALUES (@fn,@ln,@age)", conn);

            cmd.Parameters.AddWithValue("@fn", student.FirstName);
            cmd.Parameters.AddWithValue("@ln", student.LastName);
            cmd.Parameters.AddWithValue("@age", student.Age);

            cmd.ExecuteNonQuery();
        }
    }

    public void Update(Student student)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "UPDATE Students SET FirstName=@fn, LastName=@ln, Age=@age WHERE Id=@id", conn);

            cmd.Parameters.AddWithValue("@id", student.Id);
            cmd.Parameters.AddWithValue("@fn", student.FirstName);
            cmd.Parameters.AddWithValue("@ln", student.LastName);
            cmd.Parameters.AddWithValue("@age", student.Age);

            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }

   
}