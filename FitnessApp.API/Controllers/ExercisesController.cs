using FitnessApp.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{muscleGroupId}")]
        public async Task<ActionResult<IEnumerable<Exercises>>> GetByMuscleGroup(int muscleGroupId)
        {
            var exercises = await _context.Exercises
                .Where(e => e.MuscleGroupId == muscleGroupId)
                .ToListAsync();

            if (exercises == null || exercises.Count == 0)
                return NotFound();

            return exercises;
        }
    }

}
