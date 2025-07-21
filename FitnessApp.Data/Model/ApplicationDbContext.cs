using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Data.Models;

namespace FitnessApp.Data.Model
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add your DbSet<T> properties here, for example:
        // public DbSet<Workout> Workouts { get; set; }
        // public DbSet<Exercise> Exercises { get; set; }
    }
}
