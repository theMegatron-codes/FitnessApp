using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Data.Model
{
    public class Exercises
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int MuscleGroupId { get; set; }
        public MuscleGroup? MuscleGroup { get; set; }

        public ICollection<WorkoutExercise>? WorkoutExercises { get; set; }
    }

}
