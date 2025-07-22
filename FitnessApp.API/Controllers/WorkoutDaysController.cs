using FitnessApp.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.API.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class WorkoutDaysController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public WorkoutDaysController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<WorkoutDay>> Get(int id)
            {
                var day = await _context.WorkoutDays
                    .Include(wd => wd.MuscleGroup!)
                    .Include(wd => wd.WorkoutExercises!)
                        .ThenInclude(we => we.Exercise)
                    .FirstOrDefaultAsync(wd => wd.Id == id);

                if (day == null) return NotFound();

                return day;
            }

            [HttpPost]
            public async Task<ActionResult<WorkoutDay>> Create(WorkoutDay workoutDay)
            {
                _context.WorkoutDays.Add(workoutDay);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = workoutDay.Id }, workoutDay);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, WorkoutDay workoutDay)
            {
                if (id != workoutDay.Id) return BadRequest();

                _context.Entry(workoutDay).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutDayExists(id)) return NotFound();
                    else throw;
                }

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var day = await _context.WorkoutDays.FindAsync(id);
                if (day == null) return NotFound();

                _context.WorkoutDays.Remove(day);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool WorkoutDayExists(int id) =>
                _context.WorkoutDays.Any(wd => wd.Id == id);
        }

}