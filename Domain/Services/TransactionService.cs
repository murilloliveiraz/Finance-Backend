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

        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            var despesasUsuario = await _transaction.GetUserTransactions(emailUsuario);
            var despesasAnterior = await _transaction.GetUserTransactionsOverdue(emailUsuario);

            var despesas_naoPagasMesesAnteriores = despesasAnterior.Any() ?
                despesasAnterior.ToList().Sum(x => x.Value) : 0;

            var despesas_pagas = despesasUsuario.Where(d => d.AlreadyPaid && d.TransactionType == Entities.Enums.EnumTransactionType.Contas)
                .Sum(x => x.Value);

            var despesas_pendentes = despesasUsuario.Where(d => !d.AlreadyPaid && d.TransactionType == Entities.Enums.EnumTransactionType.Contas)
                .Sum(x => x.Value);

            var investimentos = despesasUsuario.Where(d => d.TransactionType == Entities.Enums.EnumTransactionType.Investimento)
                .Sum(x => x.Value);

            return new
            {
                sucesso = "OK",
                despesas_pagas = despesas_pagas,
                despesas_pendentes = despesas_pendentes,
                despesas_naoPagasMesesAnteriores = despesas_naoPagasMesesAnteriores,
                investimentos = investimentos
            };

        }
    }
}
