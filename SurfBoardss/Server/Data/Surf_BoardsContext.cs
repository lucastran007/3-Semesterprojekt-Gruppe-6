using Microsoft.EntityFrameworkCore;
using SurfBoardss.Shared;

namespace SurfBoardss.Server.Data
{
    public class Surf_BoardsContext : DbContext 
        {
            public Surf_BoardsContext(DbContextOptions<Surf_BoardsContext> options) : base(options) {
        }

        public DbSet<SurfBoard> SurfBoard { get; set; }
        public DbSet<Rental> Rental { get; set; }

    }
}
