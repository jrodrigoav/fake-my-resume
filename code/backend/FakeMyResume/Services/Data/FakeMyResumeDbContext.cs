using AppAny.Quartz.EntityFrameworkCore.Migrations;
using AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;
using FakeMyResume.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace FakeMyResume.Services.Data
{
    public class FakeMyResumeDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<DataResume> DataResume { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Note> Notes { get; set; }

        public FakeMyResumeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adds Quartz.NET SQLite schema to EntityFrameworkCore
            modelBuilder.AddQuartz(builder => builder.UseSqlite());

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.Name);
            });

            modelBuilder.Entity<AppConfig>().HasData(new AppConfig
            {
                Id = 1,
                Page = 1,
                LastUpdated = DateTime.MinValue
            });

            modelBuilder.Entity<DataResume>().OwnsOne(jsondata => jsondata.ResumeData, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
                ownedNavigationBuilder.OwnsMany(dr => dr.WorkExperience);
                ownedNavigationBuilder.OwnsMany(dr => dr.Education);
                //ownedNavigationBuilder.OwnsMany(dr => dr.Certifications);
                
            });

            modelBuilder.Entity<Note>().HasKey(n => new { n.UserId, n.CreatedAt });
        }

        public async Task UpsertTagsFromSourceAsync(Models.StackExchange.Tag[] sourceTags)
        {
            var localTags = from t in Tags.AsNoTracking().ToList()
                            join s in sourceTags on t.Name equals s.Name into gj
                            from subgroup in gj.DefaultIfEmpty()
                            where subgroup != null
                            select new Models.Data.Tag(subgroup);
            Tags.UpdateRange(localTags);
            await SaveChangesAsync();
            var newTags = sourceTags.ExceptBy(Tags.Select(t => t.Name), e => e.Name);
            await Tags.AddRangeAsync(newTags.Select(t => new Models.Data.Tag(t)));
            await SaveChangesAsync();
        }
    }
}
