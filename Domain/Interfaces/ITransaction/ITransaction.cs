using Domain.Interfaces.Generics;
using Entities.Entities;

namespace Domain.Interfaces.ITransaction
{
    public interface ITransaction : GenericInterface<Transaction>
    {
        Task<IEnumerable<Transaction>> GetUserTransactions(string userEmail);
        Task<IEnumerable<Transaction>> GetUserTransactionsOverdue(string userEmail);
    }
}
