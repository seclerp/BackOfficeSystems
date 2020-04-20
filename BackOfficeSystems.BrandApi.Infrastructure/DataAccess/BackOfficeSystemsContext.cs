using BackOfficeSystems.BrandApi.Domain.BrandAggregate;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeSystems.BrandApi.Infrastructure.DataAccess
{
    public class BackOfficeSystemsContext : DbContext
    {
        public BackOfficeSystemsContext()
        {
        }

        public BackOfficeSystemsContext(DbContextOptions<BackOfficeSystemsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BrandQuantityTimeReceived> BrandQuantityTimeReceived { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrandQuantityTimeReceived>(entity =>
            {
                entity.HasKey(e => new { e.BrandId, e.Quantity, e.TimeReceived })
                    .HasName("PRIMARY");

                entity.ToTable("Brand_Quantity_Time_Received");

                entity.Property(e => e.BrandId).HasColumnName("BRAND_ID");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.Property(e => e.TimeReceived)
                    .HasColumnName("TIME_RECEIVED")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.BrandQuantityTimeReceived)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("Brand_Quantity_Time_Received_ibfk_1");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brands");

                entity.HasKey(e => e.BrandId)
                    .HasName("PRIMARY");

                entity.Property(e => e.BrandId).HasColumnName("BRAND_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(256)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });
        }
    }
}
