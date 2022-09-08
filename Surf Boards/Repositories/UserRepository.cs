using Surf_Boards.Areas.Identity.Data;
using Surf_Boards.Core.Repository;
using Surf_Boards.Data;

namespace Surf_Boards.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Surf_BoardsContext1 _context;

        public UserRepository(Surf_BoardsContext1 context)
        {
            _context = context;
        }

        public Surf_BoardsUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<Surf_BoardsUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public Surf_BoardsUser UpdateUser(Surf_BoardsUser user)
        {
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
