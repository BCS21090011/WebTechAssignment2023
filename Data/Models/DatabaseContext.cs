﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<HistoryQuestion> HistoryQuestion { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Scaffolding:ConnectionString", "Data Source=(local);Initial Catalog=Database;Integrated Security=true");

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.HistoryId).HasColumnName("History_ID");

                entity.Property(e => e.GeneratedTime).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<HistoryQuestion>(entity =>
            {
                entity.Property(e => e.HistoryQuestionId).HasColumnName("HistoryQuestion_ID");

                entity.Property(e => e.HistoryId).HasColumnName("History_ID");

                entity.Property(e => e.QuestionNumber).HasColumnName("Question_Number");

                entity.Property(e => e.QuestionsId).HasColumnName("Questions_ID");

                entity.HasOne(d => d.History)
                    .WithMany(p => p.HistoryQuestion)
                    .HasForeignKey(d => d.HistoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Questions)
                    .WithMany(p => p.HistoryQuestion)
                    .HasForeignKey(d => d.QuestionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.Property(e => e.QuestionsId).HasColumnName("Questions_ID");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.AnswerImageFileName).IsUnicode(false);

                entity.Property(e => e.Question)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.QuestionImageFileName).IsUnicode(false);

                entity.Property(e => e.SubjectsId).HasColumnName("Subjects_ID");

                entity.HasOne(d => d.Subjects)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.SubjectsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Subjects");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.Property(e => e.SubjectsId).HasColumnName("Subjects_ID");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UsersId).HasColumnName("Users_ID");

                entity.Property(e => e.UserCountry)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserGender)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserSchoolName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}