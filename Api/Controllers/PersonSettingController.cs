#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonSettingController : ControllerBase
    {
        private readonly Context _context;

        public PersonSettingController(Context context)
        {
            _context = context;
        }

        // GET: api/PersonSettings/5
        [HttpGet("GetPersonSetting/{id}")]
        public async Task<ActionResult<PersonSetting>> GetPersonSetting(int id)
        {
            var PersonSetting = await _context.PersonSettings.FindAsync(id);

            if (PersonSetting == null)
            {
                return NotFound();
            }

            return PersonSetting;
        }

        // POST: api/PersonSettings
        [HttpPost("CreatePersonSetting")]
        public async Task<ActionResult<PersonSetting>> CreatePersonSetting(PersonSetting personSetting)
        {
            _context.PersonSettings.Add(personSetting);
            await _context.SaveChangesAsync();

            return personSetting;
        }

        // PUT: api/PersonSettings/5
        [HttpPut("UpdatePersonSetting")]
        public async Task<IActionResult> UpdatePersonSetting(PersonSetting personSetting)
        {
            if (personSetting == null)
            {
                return BadRequest();
            }

            _context.Entry(personSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonSettingExists(personSetting.PersonId))
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

        private bool PersonSettingExists(int id)
        {
            return _context.PersonSettings.Any(e => e.PersonId == id);
        }
    }
}
