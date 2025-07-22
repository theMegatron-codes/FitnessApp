using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Data.Model
{
    public class MuscleGroup
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Exercises>? Exercise { get; set; }
    }


}
