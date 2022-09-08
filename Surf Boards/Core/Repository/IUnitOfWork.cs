using Surf_Boards.Core.Repository;

namespace Surf_Boards.Core.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
    }
}
