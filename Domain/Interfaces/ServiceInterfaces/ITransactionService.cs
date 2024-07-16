using Entities.Entities;

namespace Domain.Interfaces.IService
{
    public interface ITransactionService
    {
        Task AdicionarTransacao(Transaction transacao);
        Task AtualizarTransacao(Transaction transacao);
    }
}
