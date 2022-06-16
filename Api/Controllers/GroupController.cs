#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly Context _context;

        public GroupController(Context context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet("GetGroups")]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _context.Groups.Include("Students")
                .Include("Appointments")
                .ToListAsync();
        }

        // GET: api/Groups
        [HttpGet("GetGroup/{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            return await _context.Groups.Where(x => x.Id == id)
                .Include("Students")
                .Include("Students.Grades")
                .Include("Appointments")
                .FirstOrDefaultAsync();
        }

        // POST: api/Groups
        [HttpPost("CreateGroup")]
        public async Task<ActionResult<Group>> CreateGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return group;
        }

        // PUT: api/Groups/5
        [HttpPut("UpdateGroup")]
        public async Task<IActionResult> UpdateGroup(Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            _context.Entry(group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(group.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        // DELETE: api/Groups/5
        [HttpDelete("DeleteGroup/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
