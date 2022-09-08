using Microsoft.AspNetCore.Identity;
using Surf_Boards.Areas.Identity.Data;

namespace Surf_Boards.Core.Repository
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole>GetRoles();
    }
}
