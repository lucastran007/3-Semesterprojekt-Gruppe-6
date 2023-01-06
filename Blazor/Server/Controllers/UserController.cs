using Blazor.Server.Data;
using Blazor.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Security.Claims;


namespace Blazor.Server.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context) 
        {
            _userManager = userManager;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        //Get all users
        [HttpGet]
        public async Task<List<ApplicationUser>> GetUsers()
        {
            var response = await _context.Users.Where(user => user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
            return response;
        }



        [HttpGet]
        public async Task<IdentityUser> GetUser()
        {
            return await _userManager.GetUserAsync(User);
        }

        [HttpGet]
        public async Task<string> GetUserId()
        {
            return (await _userManager.GetUserAsync(User)).Id;
        }


    }
}
