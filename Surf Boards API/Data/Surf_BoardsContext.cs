using Microsoft.EntityFrameworkCore;
using Surf_Boards.Models;

namespace Surf_Boards.Data {
    public class Surf_BoardsContext : DbContext 
        {
            public Surf_BoardsContext(DbContextOptions<Surf_BoardsContext> options) : base(options) {
        }

        public DbSet<SurfBoard> SurfBoard { get; set; }
        //public DbSet<Surf_Boards.Models.Domain.Rental> Rental { get; set; } = default!;
    }
}
