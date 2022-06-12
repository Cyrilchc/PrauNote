#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentStatusController : ControllerBase
    {
        private readonly Context _context;

        public AppointmentStatusController(Context context)
        {
            _context = context;
        }

        // GET: api/AppointmentStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentStatus>>> GetAppointmentStatuses()
        {
            return await _context.AppointmentStatuses.ToListAsync();
        }

        // GET: api/AppointmentStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentStatus>> GetAppointmentStatus(int id)
        {
            var appointmentStatus = await _context.AppointmentStatuses.FindAsync(id);

            if (appointmentStatus == null)
            {
                return NotFound();
            }

            return appointmentStatus;
        }

        // PUT: api/AppointmentStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentStatus(int id, AppointmentStatus appointmentStatus)
        {
            if (id != appointmentStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointmentStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentStatusExists(id))
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

        // POST: api/AppointmentStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppointmentStatus>> PostAppointmentStatus(AppointmentStatus appointmentStatus)
        {
            _context.AppointmentStatuses.Add(appointmentStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointmentStatus", new { id = appointmentStatus.Id }, appointmentStatus);
        }

        // DELETE: api/AppointmentStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentStatus(int id)
        {
            var appointmentStatus = await _context.AppointmentStatuses.FindAsync(id);
            if (appointmentStatus == null)
            {
                return NotFound();
            }

            _context.AppointmentStatuses.Remove(appointmentStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentStatusExists(int id)
        {
            return _context.AppointmentStatuses.Any(e => e.AppointmentStatusId == id);
        }
    }
}
