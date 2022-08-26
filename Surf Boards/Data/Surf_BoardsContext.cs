using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Surf_Boards.Models;

namespace Surf_Boards.Data
{
    public class Surf_BoardsContext : DbContext
    {
        public Surf_BoardsContext (DbContextOptions<Surf_BoardsContext> options)
            : base(options)
        {
        }

        public DbSet<Surf_Boards.Models.SurfBoard> SurfBoard { get; set; } = default!;
    }
}
