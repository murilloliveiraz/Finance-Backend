using Entities.Entities;

namespace Domain.Interfaces.ServiceInterfaces
{
    public interface IFinanceSystemService
    {
        Task AdicionarSistemaFinanceiro(FinanceSystem sistema);
        Task AtualizarSistemaFinanceiro(FinanceSystem sistema);
    }
}
