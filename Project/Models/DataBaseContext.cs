using Microsoft.EntityFrameworkCore;
using Project.Tools.Enums;

namespace Project.Models;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Visit> Visits { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Project;Username=Admin;Password=Password");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                Login = "Marlo",
                Password = "1234",
                Name = "Виталий",
                Surname = "Погодин",
                Patronymic = "Владимирович",
                Phone = "87006005040",
                Role = UserRole.Admin,
                IsCanLogin = true
            }
        );

        modelBuilder.Entity<Group>().HasData(
            new Group()
            {
                Id = 1,
                Name = "4-09пс-1"
            },
            new Group()
            {
                Id = 2,
                Name = "4-09пс-2"
            },
            new Group()
            {
                Id = 3,
                Name = "4-09пс-3"
            }
        );

        modelBuilder.Entity<Group>().Navigation(g => g.Students).AutoInclude();

        modelBuilder.Entity<Student>().HasData(
            new Student()
            {
                Id = 1,
                GroupId = 1,
                Name = "ИмяСтудента1"
            }
        );
    }
}