using Blazor.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SurfboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurfboardController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET ALL SURFBOARDS
        [HttpGet]
        public async Task<ActionResult> GetAllSurfBoards()
        {
            var surfboards = await _context.SurfBoard.ToArrayAsync();
            return Ok(surfboards);
        }

        // GET SURFBOARD BY GUID
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSurfBoard(Guid id)
        {
            var surfboard = await _context.SurfBoard.FindAsync(id);
            if (surfboard == null)
                return NotFound();

            return Ok(surfboard);
        }
    }
}
