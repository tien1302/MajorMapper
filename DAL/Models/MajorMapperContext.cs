using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class MajorMapperContext : DbContext
{
    public MajorMapperContext()
    {
    }

    public MajorMapperContext(DbContextOptions<MajorMapperContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PersonalityType> PersonalityTypes { get; set; }

    public virtual DbSet<ReviewTest> ReviewTests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = MajorMapper;uid=sa;pwd=123456;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedDateTime).HasColumnType("date");
            entity.Property(e => e.DoB).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Role");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.EndDateTime).HasColumnType("date");
            entity.Property(e => e.StartDateTime).HasColumnType("date");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Consultant).WithMany(p => p.BookingConsultants)
                .HasForeignKey(d => d.ConsultantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Account1");

            entity.HasOne(d => d.Student).WithMany(p => p.BookingStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Account");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.ToTable("Major");

            entity.Property(e => e.CreatedDateTime).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("date");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.Property(e => e.Time)
                .HasColumnType("date")
                .HasColumnName("time");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Booking).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_Notification_Booking");
        });

        modelBuilder.Entity<PersonalityType>(entity =>
        {
            entity.ToTable("PersonalityType");

            entity.Property(e => e.CreatedDateTime).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("date");

            entity.HasMany(d => d.Majors).WithMany(p => p.PersonalityTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonalityTypeMajor",
                    r => r.HasOne<Major>().WithMany()
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PersonalityType_Major_Major"),
                    l => l.HasOne<PersonalityType>().WithMany()
                        .HasForeignKey("PersonalityTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PersonalityType_Major_PersonalityType"),
                    j =>
                    {
                        j.HasKey("PersonalityTypeId", "MajorId");
                        j.ToTable("PersonalityType_Major");
                    });
        });

        modelBuilder.Entity<ReviewTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ReviewAnalysis");

            entity.ToTable("ReviewTest");

            entity.Property(e => e.CreatedDateTime).HasColumnType("date");

            entity.HasOne(d => d.TestResult).WithMany(p => p.ReviewTests)
                .HasForeignKey(d => d.TestResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReviewAnalysis_TestResult");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => new { e.TestResultId, e.PersonalityTypeId });

            entity.ToTable("Score");

            entity.HasOne(d => d.PersonalityType).WithMany(p => p.Scores)
                .HasForeignKey(d => d.PersonalityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Score_PersonalityType");

            entity.HasOne(d => d.TestResult).WithMany(p => p.Scores)
                .HasForeignKey(d => d.TestResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Score_TestResult");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.ToTable("TestResult");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreatedDateTime).HasColumnType("date");

            entity.HasOne(d => d.Account).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestResult_Account");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.ToTable("University");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedDateTime).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("date");
            entity.Property(e => e.Website).HasMaxLength(200);

            entity.HasMany(d => d.Majors).WithMany(p => p.Universities)
                .UsingEntity<Dictionary<string, object>>(
                    "UniversityMajor",
                    r => r.HasOne<Major>().WithMany()
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_University_Major_Major"),
                    l => l.HasOne<University>().WithMany()
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_University_Major_University"),
                    j =>
                    {
                        j.HasKey("UniversityId", "MajorId");
                        j.ToTable("University_Major");
                        j.IndexerProperty<int>("UniversityId").HasColumnName("UniversityID");
                        j.IndexerProperty<int>("MajorId").HasColumnName("MajorID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
