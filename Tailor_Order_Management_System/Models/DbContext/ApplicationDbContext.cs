using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Tailor_Order_Management_System.Models.EntityClasses;

namespace Tailor_Order_Management_System.Models.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Fabric>()
       .Property(f => f.quantity)
       .HasColumnType("decimal(18,2)");
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.RefreshTokens)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            //Configure the owned entity
            // Configure the owned entity
            //builder.Entity<ApplicationUser>()
            //    .OwnsMany(u => u.RefreshTokens, rt =>
            //    {
            //        rt.WithOwner(t => t.User)
            //          .HasForeignKey(t => t.UserId);
            //    });



        }


        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Fabric> Fabrics { get; set; } = null!;

        public DbSet<Color> Colors { get; set; } = null!;
        public DbSet<FabricColor> FabricColors { get; set; } = null!;
    }
}
