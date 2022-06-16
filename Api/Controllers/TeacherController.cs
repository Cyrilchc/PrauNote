#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly Context _context;

        public TeacherController(Context context)
        {
            _context = context;
        }

        // GET: api/Teachers/GetTeachers
        [HttpGet("GetTeachers")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("GetTeacher/{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var Teacher = await _context.Teachers.FindAsync(id);

            if (Teacher == null)
            {
                return NotFound();
            }

            return Teacher;
        }

        // POST: api/Teachers
        [HttpPost("CreateTeacher")]
        public async Task<ActionResult<Teacher>> CreateTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return teacher;
        }

        // PUT: api/Teachers/5
        [HttpPut("UpdateTeacher")]
        public async Task<IActionResult> UpdateTeacher(Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(teacher.Id))
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

        // DELETE: api/Teachers/5
        [HttpDelete("DeleteTeacher/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var Teacher = await _context.Teachers.FindAsync(id);
            if (Teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(Teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
