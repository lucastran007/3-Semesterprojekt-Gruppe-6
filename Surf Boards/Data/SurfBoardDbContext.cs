using Microsoft.EntityFrameworkCore;
using Surf_Boards.Models;

namespace Surf_Boards.Data
{
    public class SurfBoardDbContext:DbContext
    {
        public SurfBoardDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<SurfBoard> SurfBoards { get; set; }
    }
}
