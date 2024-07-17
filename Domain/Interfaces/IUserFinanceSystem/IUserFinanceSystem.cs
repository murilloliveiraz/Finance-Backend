using Domain.Interfaces.Generics;
using Entities.Entities;

namespace Domain.Interfaces.IUserFinanceSystem
{
    public interface IUserFinanceSystem : GenericInterface<UserFinanceSystem>
    {
        Task<IEnumerable<UserFinanceSystem>> GetSystemUsers(int idSystem);
        Task RemoveSystemUsers(IEnumerable<UserFinanceSystem> users);
        Task <UserFinanceSystem> GetUserByEmail(string userEmail);
    }
}
