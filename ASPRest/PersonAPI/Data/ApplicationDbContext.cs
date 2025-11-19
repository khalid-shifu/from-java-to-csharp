using Microsoft.EntityFrameworkCore;
using PersonAPI.Models;

namespace PersonAPI.Data;

// Like extending JpaRepository in Spring Boot
public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet = Table in database
    public DbSet<Person> Persons { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}
