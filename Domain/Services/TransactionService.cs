using Domain.Interfaces.IService;
using Domain.Interfaces.ITransaction;
using Entities.Entities;

namespace Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransaction _transaction;

        public TransactionService(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task AdicionarTransacao(Transaction transacao)
        {
            var data = DateTime.UtcNow;
            transacao.RegistrationDate = data;
            transacao.Year = data.Year;
            transacao.Month = data.Month;
            var valid = transacao.ValidateStringProperty(transacao.Name, "Nome");
            if (valid)
            {
                await _transaction.Add(transacao);
            }
        }

        public async Task AtualizarTransacao(Transaction transacao)
        {
            var data = DateTime.UtcNow;
            transacao.DateOfTheChange = data;
            if(transacao.AlreadyPaid)
            {
                transacao.PaymentDate = data;
            }
            var valid = transacao.ValidateStringProperty(transacao.Name, "Nome");
            if (valid)
            {
                await _transaction.Update(transacao);
            }
        }
    }
}
