using BMC.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BMC.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BusinessModelCanvas> BusinessModelCanvases { get; set; }
        public DbSet<PostIt> PostIts { get; set; }
        public DbSet<UserCanvasAssociation> UserCanvasAssociations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCanvasAssociation>()
                .HasKey(uc => new { uc.UserId, uc.CanvasId });

            modelBuilder.Entity<UserCanvasAssociation>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCanvases)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCanvasAssociation>()
                .HasOne(uc => uc.Canvas)
                .WithMany(c => c.UserCanvases)
                .HasForeignKey(uc => uc.CanvasId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostIt>()
                .HasOne<BusinessModelCanvas>() 
                .WithMany(c => c.PostIts) 
                .HasForeignKey(p => p.CanvasId); 
            
            modelBuilder.Ignore<IdentityUserLogin<string>>();
        }
    }
}

