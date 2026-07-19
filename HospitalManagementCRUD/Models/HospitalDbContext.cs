using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementCRUD.Models;

public partial class HospitalDbContext : DbContext
{
    public HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<EnumTable> EnumTables { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<SecLoginUser> SecLoginUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__APPOINTM__8ECDFCC244D5F561");

            entity.ToTable("APPOINTMENT", "demo");

            entity.Property(e => e.DiseaseDescription).HasMaxLength(200);
            entity.Property(e => e.FeesPaid).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.LastModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorId_Appointment");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__APPOINTME__UserI__19DFD96B");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__DOCTOR__2DC00EBFFAFDCCD5");

            entity.ToTable("DOCTOR", "demo");

            entity.Property(e => e.ConsultationFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DoctorName).HasMaxLength(100);
            entity.Property(e => e.Qualification).HasMaxLength(100);

            entity.HasOne(d => d.Hospital).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK_HospitalId_Doctor");

            entity.HasOne(d => d.User).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DOCTOR__UserId__18EBB532");
        });

        modelBuilder.Entity<EnumTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Enum_Tab__3214EC07BFA57C2F");

            entity.ToTable("Enum_Table", "demo");

            entity.Property(e => e.DisplayText).HasMaxLength(100);
            entity.Property(e => e.EnumGroup).HasMaxLength(100);
            entity.Property(e => e.LastModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Value).HasMaxLength(100);
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__HOSPITAL__38C2E5AF897C320B");

            entity.ToTable("HOSPITAL", "demo");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HospitalName).HasMaxLength(250);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.State).HasMaxLength(100);
        });

        modelBuilder.Entity<SecLoginUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__SecLogin__1788CC4CC46D7747");

            entity.ToTable("SecLoginUser", "demo");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UserName).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
