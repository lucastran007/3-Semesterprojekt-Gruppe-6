using Microsoft.AspNetCore.Identity;
using Surf_Boards.Core.Repository;
using Surf_Boards.Data;

namespace Surf_Boards.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly Surf_BoardsContext1 _context;

        public RoleRepository(Surf_BoardsContext1 context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
