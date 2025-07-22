using FitnessApp.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkoutExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutExercise>> Get(int id)
        {
            var exercise = await _context.WorkoutExercises
                .Include(we => we.Exercise)
                .Include(we => we.WorkoutDay)
                .FirstOrDefaultAsync(we => we.Id == id);

            if (exercise == null) return NotFound();

            return exercise;
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutExercise>> Create(WorkoutExercise workoutExercise)
        {
            _context.WorkoutExercises.Add(workoutExercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = workoutExercise.Id }, workoutExercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkoutExercise workoutExercise)
        {
            if (id != workoutExercise.Id) return BadRequest();

            _context.Entry(workoutExercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExerciseExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var workoutExercise = await _context.WorkoutExercises.FindAsync(id);
            if (workoutExercise == null) return NotFound();

            _context.WorkoutExercises.Remove(workoutExercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkoutExerciseExists(int id) =>
            _context.WorkoutExercises.Any(we => we.Id == id);
    }

}