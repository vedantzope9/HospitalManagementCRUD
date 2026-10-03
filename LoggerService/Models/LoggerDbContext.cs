using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoggerService.Models;

public partial class LoggerDbContext : DbContext
{
    public LoggerDbContext()
    {
    }

    public LoggerDbContext(DbContextOptions<LoggerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppExceptionLog> AppExceptionLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppExceptionLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__APP_EXCE__5E54864819ECE543");

            entity.ToTable("APP_EXCEPTION_LOG", "demo");

            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(sysutcdatetime())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
