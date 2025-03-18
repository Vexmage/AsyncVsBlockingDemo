using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

// Define the database model
public class ApiResponse
{
    public int Id { get; set; }
    public string Data { get; set; }
    public DateTime RetrievedAt { get; set; }
}

// Define the database context
public class DatabaseContext : DbContext
{
    public DbSet<ApiResponse> ApiResponses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=AsyncDemo.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiResponse>().ToTable("ApiResponses"); // Ensure table name matches
    }

    public async Task InitializeDatabaseAsync()
    {
        Console.WriteLine("🔄 Applying Migrations...");
        await Database.MigrateAsync();  // Ensure database is updated with migrations
        Console.WriteLine("Migrations Applied!");
    }
}
