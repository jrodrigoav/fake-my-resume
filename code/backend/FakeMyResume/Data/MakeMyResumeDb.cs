using FakeMyResume.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeMyResume.Data;

public class MakeMyResumeDb : DbContext
{
    private readonly string? _connection;

    public virtual DbSet<DataResume> DataResume { get; set; }

    public  DbSet<User> User { get; set; }

    public MakeMyResumeDb(IConfiguration configuration) : base() 
    {
        _connection = configuration.GetConnectionString("MyResume");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataResume>(entity =>
        {
            entity.ToTable("DataResume");
            entity.HasKey("Id");
            entity.HasOne(r => r.User)
                .WithMany(u => u.Resumes)
                .HasForeignKey(r => r.AccountId);
        });
    }
}
