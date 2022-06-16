#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly Context _context;

        public ChatController(Context context)
        {
            _context = context;
        }

        // GET: api/Chats/GetChats
        [HttpGet("GetCurrentChats")]
        public async Task<ActionResult<IEnumerable<Chat>>> GetCurrentChats()
        {
            return await _context.Chats.Where(x => x.StartDateTime < DateTime.Now && x.EndDateTime > DateTime.Now)
                .ToListAsync();
        }

        // GET: api/Chats/GetChats
        [HttpGet("GetOldChats")]
        public async Task<ActionResult<IEnumerable<Chat>>> GetOldChats()
        {
            return await _context.Chats.Where(x => x.EndDateTime < DateTime.Now)
                .ToListAsync();
        }

        // GET: api/Chats/GetChats
        [HttpGet("GetUpcomingChats")]
        public async Task<ActionResult<IEnumerable<Chat>>> GetUpcomingChats()
        {
            return await _context.Chats.Where(x => x.StartDateTime > DateTime.Now)
                .ToListAsync();
        }

        // POST: api/Chats
        [HttpPost("CreateChat")]
        public async Task<ActionResult<Chat>> CreateChat(Chat chat)
        {
            _context.Chats.Add(chat);
            //_context.ChatAffectations.AddRange(chat.ChatAffectations.ToList());

            await _context.SaveChangesAsync();


            return chat;
        }

        // PUT: api/Chats/5
        [HttpPut("UpdateChat")]
        public async Task<IActionResult> UpdateChat(Chat chat)
        {
            if (chat == null)
            {
                return BadRequest();
            }

            _context.Entry(chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(chat.Id))
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

        // DELETE: api/Chats/5
        [HttpDelete("DeleteChat/{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatExists(int id)
        {
            return _context.Chats.Any(e => e.Id == id);
        }
    }
}
