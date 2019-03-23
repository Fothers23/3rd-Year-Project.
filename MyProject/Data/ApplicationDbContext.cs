using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // This allows the Games and Reviews to link correctly.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>()
                .HasMany<Review>()
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany<Game>()
                .WithOne(x => x.Developer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany<Review>()
                .WithOne(x => x.user);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MyProject.Models.ApplicationRole> ApplicationRole { get; set; }
    }
}
