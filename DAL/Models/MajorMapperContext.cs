using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PersonalityType> PersonalityTypes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<ReviewTest> ReviewTests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestQuestion> TestQuestions { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBStore"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
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

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Slot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Slot");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Account");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__feedback__3214EC07F4DEEA96");

            entity.ToTable("Feedback");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_Booking");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.ToTable("Major");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Booking).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_Notification_Booking");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1);
            entity.Property(e => e.OrderType).HasMaxLength(1);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Relatied).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RelatiedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Booking");

            entity.HasOne(d => d.RelatiedNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RelatiedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_TestResult");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Account");
        });

        modelBuilder.Entity<PersonalityType>(entity =>
        {
            entity.ToTable("PersonalityType");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

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

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<ReviewTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ReviewAnalysis");

            entity.ToTable("ReviewTest");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

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

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.ToTable("Slot");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Consultant).WithMany(p => p.Slots)
                .HasForeignKey(d => d.ConsultantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Slot_Account");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.ToTable("Test");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Tests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_Account");
        });

        modelBuilder.Entity<TestQuestion>(entity =>
        {
            entity.HasKey(e => new { e.TestId, e.QuestionId });

            entity.ToTable("Test_Question");

            entity.HasOne(d => d.PersonalityType).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.PersonalityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_Question_PersonalityType");

            entity.HasOne(d => d.Question).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_Question_Question");

            entity.HasOne(d => d.Test).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_Question_Test");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.ToTable("TestResult");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestResult_Test");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
