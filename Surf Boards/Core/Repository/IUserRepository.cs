using Surf_Boards.Areas.Identity.Data;

namespace Surf_Boards.Core.Repository
{
    public interface IUserRepository
    {

        ICollection<Surf_BoardsUser> GetUsers();

        Surf_BoardsUser GetUser(string id);

        Surf_BoardsUser UpdateUser(Surf_BoardsUser user);
    }
}
