using FakeMyResume.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeMyResume.Data;

public class TagsDbContext(DbContextOptions<TagsDbContext> options) : DbContext(options)
{
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Name);
        });
    }
}
