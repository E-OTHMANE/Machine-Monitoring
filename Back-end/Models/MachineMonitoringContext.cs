using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Models
{
    public partial class MachineMonitoringContext : DbContext
    {
        public MachineMonitoringContext()
        {
        }

        public MachineMonitoringContext(DbContextOptions<MachineMonitoringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<MachineProduction> MachineProductions { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.ToTable("Machine");

                entity.Property(e => e.MachineId).HasColumnName("machineId");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MachineProduction>(entity =>
            {
                entity.ToTable("MachineProduction");

                entity.Property(e => e.TotalProduction).HasColumnName("totalProduction");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.MachineProductions)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MachineProduction_Machine");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
