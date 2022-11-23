using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfBoards.Server.Data;

namespace SurfBoards.Server.Controllers
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/surfboards")]
    [ApiController]
    public class SurfboardsV1Controller : ControllerBase {
        private readonly Surf_BoardsContext _context;

        public SurfboardsV1Controller(Surf_BoardsContext context) {
            _context = context;
        }
    

        // GET ALL SURFBOARDS
        [HttpGet]
        public async Task<ActionResult> GetAllSurfBoards() {
            var surfboards = await _context.SurfBoard.ToArrayAsync();
            return Ok(surfboards);
        }

        // GET SURFBOARD BY GUID
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSurfBoard(Guid id) {
            var surfboard = await _context.SurfBoard.FindAsync(id);
            if (surfboard == null)
                return NotFound();

            return Ok(surfboard);
        }
        
    }
}
