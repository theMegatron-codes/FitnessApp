using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Data.Model
{
    public class WorkoutExercise
    {
        public int Id { get; set; }

        public int WorkoutDayId { get; set; }
        public WorkoutDay? WorkoutDay { get; set; }

        public int ExerciseId { get; set; }
        public Exercises? Exercise { get; set; }

        // User-entered details
        public int Reps { get; set; }
        public int Sets { get; set; }
        public float Weight { get; set; }
        public TimeSpan? Duration { get; set; }

    }
}
