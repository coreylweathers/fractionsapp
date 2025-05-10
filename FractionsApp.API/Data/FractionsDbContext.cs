using FractionsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace FractionsApp.API.Data
{
    public class FractionsDbContext : DbContext
    {
        public FractionsDbContext(DbContextOptions<FractionsDbContext> options) : base(options)
        {
        }

        public DbSet<UserProgressModel> UserProgress { get; set; } = null!;
        public DbSet<ProblemSetModel> ProblemSets { get; set; } = null!;
        public DbSet<FractionProblemModel> FractionProblems { get; set; } = null!;
        public DbSet<FractionModel> Fractions { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure UserProgressModel
            modelBuilder.Entity<UserProgressModel>()
                .HasKey(p => p.Id);
                
            modelBuilder.Entity<UserProgressModel>()
                .Property(p => p.UserId)
                .IsRequired();
                
            modelBuilder.Entity<UserProgressModel>()
                .Property(p => p.ActivityType)
                .IsRequired();
                
            // Configure ProblemSetModel
            modelBuilder.Entity<ProblemSetModel>()
                .HasKey(p => p.Id);
                
            modelBuilder.Entity<ProblemSetModel>()
                .Property(p => p.Name)
                .IsRequired();
                
            modelBuilder.Entity<ProblemSetModel>()
                .HasMany(p => p.Problems)
                .WithOne()
                .HasForeignKey(p => p.ProblemSetId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Configure FractionProblemModel
            modelBuilder.Entity<FractionProblemModel>()
                .HasKey(p => p.Id);
                
            modelBuilder.Entity<FractionProblemModel>()
                .Property(p => p.Question)
                .IsRequired();
                
            modelBuilder.Entity<FractionProblemModel>()
                .HasOne(p => p.Operand1)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<FractionProblemModel>()
                .HasOne(p => p.Operand2)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configure FractionModel
            modelBuilder.Entity<FractionModel>()
                .HasKey(f => f.Id);
                
            modelBuilder.Entity<FractionModel>()
                .Property(f => f.Denominator)
                .IsRequired();
                
            // Convert string array to JSON for storage with proper value comparer
            var optionsConverter = new ValueConverter<string[], string>(
                v => string.Join(";", v ?? Array.Empty<string>()),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries));
                
            var optionsComparer = new ValueComparer<string[]>(
                (a, b) => (a == null && b == null) || (a != null && b != null && a.SequenceEqual(b)),
                v => v == null ? 0 : v.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                v => v == null ? Array.Empty<string>() : v.ToArray());
                
            modelBuilder.Entity<FractionProblemModel>()
                .Property(p => p.Options)
                .HasConversion(optionsConverter)
                .Metadata.SetValueComparer(optionsComparer);
                
            base.OnModelCreating(modelBuilder);
        }
    }
}