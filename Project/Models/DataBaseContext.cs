using Microsoft.EntityFrameworkCore;
using Project.Tools.Enums;

namespace Project.Models;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Project;Trusted_Connection=True;TrustServerCertificate=True"); // Для локальной базы данных
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                Login = "login1",
                Password = "pass1",
                Name = "Имя1",
                Surname = "Фамилия1",
                Patronymic = "Отчество1",
                Phone = "87006005040"
            },
            new User()
            {
                Id = 2,
                Login = "login2",
                Password = "pass2",
                Name = "Имя2",
                Surname = "Фамилия2",
                Patronymic = "Отчество2",
                Phone = "87006005040",
                Role = UserRole.Admin
            }
            );
    }
}