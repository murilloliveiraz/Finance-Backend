using Domain.Interfaces.IFinanceSystem;
using Domain.Interfaces.ServiceInterfaces;
using Entities.Entities;

namespace Domain.Services
{
    public class FinanceSystemService : IFinanceSystemService
    {
        private readonly IFinanceSystem _financeSystem;

        public FinanceSystemService(IFinanceSystem financeSystem)
        {
            _financeSystem = financeSystem;
        }

        public async Task AdicionarSistemaFinanceiro(FinanceSystem sistema)
        {
            var valid = sistema.ValidateStringProperty(sistema.Name, "Nome");
            if (valid)
            {
                var data = DateTime.UtcNow;
                sistema.Closingdate = 1;
                sistema.Year = data.Year;
                sistema.Month = data.Month;
                sistema.CopyYear = data.Year;
                sistema.CopyMonth = data.Month;
                sistema.GenerateExpensesCopy = true;

                await _financeSystem.Add(sistema);
            }
        }

        public async Task AtualizarSistemaFinanceiro(FinanceSystem sistema)
        {
            var valid = sistema.ValidateStringProperty(sistema.Name, "Nome");
            if (valid)
            {
                sistema.Closingdate = 1;
                await _financeSystem.Update(sistema);
            }
        }
    }
}
