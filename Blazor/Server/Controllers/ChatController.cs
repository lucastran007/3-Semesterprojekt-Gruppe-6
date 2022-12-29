using Blazor.Server.Data;
using Blazor.Shared;
using Blazor.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Blazor.Server.Controllers {


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChatController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync() {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            var allUsers = await _context.Users.Where(user => user.Id != userId).ToListAsync();
            return Ok(allUsers);
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserDetailsAsync(string userId) {
            var user = await _context.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessageAsync(ChatMessage message) {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            message.FromUserId = userId;
            message.CreatedDate = DateTime.Now;
            message.ToUser = await _context.Users.Where(user => user.Id == message.ToUserId).FirstOrDefaultAsync();
            await _context.ChatMessages.AddAsync(message);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetConversationAsync(string contactId) {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            var messages = await _context.ChatMessages
                    .Where(h => (h.FromUserId == contactId && h.ToUserId == userId) || (h.FromUserId == userId && h.ToUserId == contactId))
                    .OrderBy(a => a.CreatedDate)
                    .Include(a => a.FromUser)
                    .Include(a => a.ToUser)
                    .Select(x => new ChatMessage
                    {
                        FromUserId = x.FromUserId,
                        Message = x.Message,
                        CreatedDate = x.CreatedDate,
                        Id = x.Id,
                        ToUserId = x.ToUserId,
                        ToUser = x.ToUser,
                        FromUser = x.FromUser
                    }).ToListAsync();
            return Ok(messages);
        }


    }

}
