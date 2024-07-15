using Domain.Interfaces.ITransaction;
using Entities.Entities;
using Infraestructure.Configurations;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class RepositoryTransaction : RepositoryGeneric<Transaction>, ITransaction
    {
        private readonly DbContextOptions<ApplicationContext> _optionsBuilder;
        public RepositoryTransaction()
        {
            _optionsBuilder = new DbContextOptions<ApplicationContext>();
        }
        public async Task<IEnumerable<Transaction>> GetUserTransactions(string userEmail)
        {
            using (var database = new ApplicationContext(_optionsBuilder))
            {
                return await
                    (from s in database.SistemaFinanceiro
                     join c in database.Categoria on s.Id equals c.IdSystem
                     join us in database.UsuarioSistemaFinanceiro on s.Id equals us.IdSystem
                     join t in database.Transacao on c.Id equals t.IdCategory
                     where us.Email.Equals(userEmail) && s.Month == t.Month && s.Year == t.Year
                     select t).AsNoTracking().ToListAsync();
            }
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactionsOverdue(string userEmail)
        {
            using (var database = new ApplicationContext(_optionsBuilder))
            {
                return await
                    (from s in database.SistemaFinanceiro
                     join c in database.Categoria on s.Id equals c.IdSystem
                     join us in database.UsuarioSistemaFinanceiro on s.Id equals us.IdSystem
                     join t in database.Transacao on c.Id equals t.IdCategory
                     where us.Email.Equals(userEmail) && t.Month < DateTime.Now.Month && !t.AlreadyPaid
                     select t).AsNoTracking().ToListAsync();
            }
        }
    }
}
