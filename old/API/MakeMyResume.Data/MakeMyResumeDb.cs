using MakeMyResume.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MakeMyResume.Data
{
    public class MakeMyResumeDb : DbContext
    {
        private readonly string _connection;
        public virtual DbSet<DataResume> DataResume { get; set; }

        public DbSet<Tag> Tag { get; set; }
        public MakeMyResumeDb(IConfiguration configuration) : base() 
        {
            _connection = configuration["ConnectionStrings:MyResume"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection, b => b.MigrationsAssembly("MakeMyResume.API"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataResume>(entity =>
            {
                entity.ToTable("DataResume");
                entity.HasKey("Id");
            });
        }
    }
}
