using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NZWalks.API.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Walk>()
                .HasOne(w => w.Difficulty)
                .WithMany()
                .HasForeignKey(w => w.DifficulyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Walk>()
                .HasOne(w => w.Region)
                .WithMany()
                .HasForeignKey(w => w.RegionId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
