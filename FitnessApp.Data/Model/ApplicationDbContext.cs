using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Data.Models;
using System.Reflection.Emit;

namespace FitnessApp.Data.Model
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Exercises> Exercises { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WorkoutExercise>()
                .HasOne(we => we.WorkoutDay)
                .WithMany(wd => wd.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutDayId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(we => we.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}