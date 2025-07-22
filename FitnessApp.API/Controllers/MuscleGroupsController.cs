using FitnessApp.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FitnessApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MuscleGroupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MuscleGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MuscleGroup>>> Get()
        {
            return await _context.MuscleGroups.ToListAsync();
        }
    }

}
