using Microsoft.EntityFrameworkCore;

namespace Project.Models;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Project;Trusted_Connection=True;TrustServerCertificate=True"); // Для локальной базы данных
    }
}