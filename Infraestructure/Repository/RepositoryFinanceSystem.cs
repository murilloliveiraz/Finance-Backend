using Domain.Interfaces.IFinanceSystem;
using Entities.Entities;
using Infraestructure.Configurations;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class RepositoryFinanceSystem : RepositoryGeneric<FinanceSystem>, IFinanceSystem
    {
        private readonly DbContextOptions<ApplicationContext> _optionsBuilder;
        public RepositoryFinanceSystem()
        {
            _optionsBuilder = new DbContextOptions<ApplicationContext>();
        }
        public async Task<IEnumerable<FinanceSystem>> GetUserSystems(string userEmail)
        {
            using (var database = new ApplicationContext(_optionsBuilder))
            {
                return await
                    (from s in database.SistemaFinanceiro
                     join us in database.UsuarioSistemaFinanceiro on s.Id equals us.IdSystem
                     where us.Email.Equals(userEmail)
                     select s).AsNoTracking().ToListAsync();
            }
        }
    }
}
