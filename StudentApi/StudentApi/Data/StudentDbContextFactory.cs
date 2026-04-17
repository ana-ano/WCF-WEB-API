using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudentApi.Data
{
    public class StudentDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
    {
        public StudentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentDbContext>();

            optionsBuilder.UseSqlServer("Server=DESKTOP-SE1JF07\\SQLEXPRESS;Database=MyStudentsDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new StudentDbContext(optionsBuilder.Options);
        }
    }
}