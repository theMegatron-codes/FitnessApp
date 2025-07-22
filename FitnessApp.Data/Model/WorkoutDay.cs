using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Data.Model
{
    public class WorkoutDay
    {
        public int Id { get; set; }
        public int DayNumber { get; set; } // e.g. 1, 2, 3...

        public int WorkoutPlanId { get; set; }
        public WorkoutPlan? WorkoutPlan { get; set; }

        public int MuscleGroupId { get; set; }
        public MuscleGroup? MuscleGroup { get; set; }

        public ICollection<WorkoutExercise>? WorkoutExercises { get; set; }
    }

}
