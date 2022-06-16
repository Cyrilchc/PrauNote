#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly Context _context;

        public GradeController(Context context)
        {
            _context = context;
        }

        // GET: api/Grades
        [HttpGet("GetStudentGrades/{id}")]
        public async Task<ActionResult<IEnumerable<Grade>>> GetStudentGrades(int id)
        {
            return await _context.Grades.Include("Subject")
                .Where(x => x.StudentId == id)
                .ToListAsync();
        }

        // POST: api/Grades
        [HttpPost("CreateGrade")]
        public async Task<ActionResult<Grade>> CreateGrade(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return grade;
        }

        // PUT: api/Grades/5
        [HttpPut("UpdateGrade")]
        public async Task<IActionResult> UpdateGrade(Grade grade)
        {
            if (grade == null)
            {
                return BadRequest();
            }

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(grade.Id))
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

        // DELETE: api/Grades/5
        [HttpDelete("DeleteGrade/{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
