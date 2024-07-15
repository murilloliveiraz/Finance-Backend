using Domain.Interfaces.Generics;
using Entities.Entities;

namespace Domain.Interfaces.IFinanceSystem
{
    public interface IFinanceSystem : GenericInterface<FinanceSystem>
    {
        Task<IEnumerable<FinanceSystem>> GetUserSystems(string userEmail);
    }
}
