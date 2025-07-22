using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.Data.Models;

namespace FitnessApp.Data.Model
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<WorkoutDay>? WorkoutDays { get; set; }
    }

}
