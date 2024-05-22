using Microsoft.EntityFrameworkCore;

namespace Project.Models;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=Project;Username=Admin;Password=Password");
    }
}