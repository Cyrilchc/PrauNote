#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly Context _context;

        public AppointmentController(Context context)
        {
            _context = context;
        }

        // GET: api/Appointment/GetPersonAppointment
        [HttpGet("GetPersonAppointment/{id}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetPersonAppointment(int id)
        {
            return await _context.Appointments.Where(x => x.PersonId == id).ToListAsync();
        }

        // GET: api/Appointment/GetGroupAppointment
        [HttpGet("GetGroupAppointment/{id}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetGroupAppointment(int id)
        {
            return await _context.Appointments.Where(x => x.GroupID == id).ToListAsync();
        }

        // POST: api/Appointment/CreateAppointment
        [HttpPost("CreateAppointment")]
        public async Task<ActionResult<Appointment>> CreateAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Appointment/UpdateAppointment
        [HttpPut("UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(appointment.Id))
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

        // DELETE: api/DeleteAppointment/5
        [HttpDelete("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var Appointment = await _context.Appointments.FindAsync(id);
            if (Appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(Appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
