using FitnessApp.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.API.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class WorkoutPlansController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public WorkoutPlansController(ApplicationDbContext context)
            {
                _context = context;
            }


            [HttpGet("{id}")]
            public async Task<ActionResult<WorkoutPlan>> Get(int id)
            {
                var plan = await _context.WorkoutPlans
                    .Include(wp => wp.WorkoutDays!)
                        .ThenInclude(wd => wd.MuscleGroup!)
                    .Include(wp => wp.WorkoutDays!)
                        .ThenInclude(wd => wd.WorkoutExercises!)
                            .ThenInclude(we => we.Exercise)
                    .FirstOrDefaultAsync(wp => wp.Id == id);

                if (plan == null) return NotFound();

                return plan;
            }


            [HttpPost]
            public async Task<ActionResult<WorkoutPlan>> Create(WorkoutPlan workoutPlan)
            {
                workoutPlan.CreatedDate = DateTime.UtcNow;

                _context.WorkoutPlans.Add(workoutPlan);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = workoutPlan.Id }, workoutPlan);
            }


            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, WorkoutPlan workoutPlan)
            {
                if (id != workoutPlan.Id)
                    return BadRequest();

                _context.Entry(workoutPlan).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutPlanExists(id))
                        return NotFound();
                    else throw;
                }

                return NoContent();
            }


            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var plan = await _context.WorkoutPlans.FindAsync(id);
                if (plan == null) return NotFound();

                _context.WorkoutPlans.Remove(plan);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool WorkoutPlanExists(int id) =>
                _context.WorkoutPlans.Any(wp => wp.Id == id);
        }

}
