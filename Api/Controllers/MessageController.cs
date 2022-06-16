#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly Context _context;

        public MessageController(Context context)
        {
            _context = context;
        }

        // GET: api/Messages/GetMessages
        [HttpGet("GetChatMessages/{id}")]
        public async Task<ActionResult<Chat>> GetChatMessages(int id)
        {
            return await _context.Chats.Where(x => x.Id == id)
                .Include("Messages")
                .Include("Messages.From")
                .FirstOrDefaultAsync();
        }

        // GET: api/Messages/5
        [HttpGet("GetMessage/{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var Message = await _context.Messages.FindAsync(id);

            if (Message == null)
            {
                return NotFound();
            }

            return Message;
        }

        // POST: api/Messages
        [HttpPost("CreateMessage")]
        public async Task<ActionResult<Message>> CreateMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        // PUT: api/Messages/5
        [HttpPut("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage(Message message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(message.Id))
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

        // DELETE: api/Messages/5
        [HttpDelete("DeleteMessage/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
