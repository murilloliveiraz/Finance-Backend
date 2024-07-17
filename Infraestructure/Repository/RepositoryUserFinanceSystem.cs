using Domain.Interfaces.IUserFinanceSystem;
using Entities.Entities;
using Infraestructure.Configurations;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class RepositoryUserFinanceSystem : RepositoryGeneric<UserFinanceSystem>, IUserFinanceSystem
    {
        private readonly DbContextOptions<ApplicationContext> _optionsBuilder;
        public RepositoryUserFinanceSystem()
        {
            _optionsBuilder = new DbContextOptions<ApplicationContext>();
        } 
        public async Task<IEnumerable<UserFinanceSystem>> GetSystemUsers(int idSystem)
        {
            using (var database = new ApplicationContext(_optionsBuilder))
            {
                return await
                    database.UsuarioSistemaFinanceiro
                    .Where(s => s.IdSystem == idSystem).AsNoTracking().ToListAsync();
            }
        }

        public async Task<UserFinanceSystem> GetUserByEmail(string userEmail)
        {
            using (var database = new ApplicationContext(_optionsBuilder))
            {
                return await
                    database.UsuarioSistemaFinanceiro
                    .AsNoTracking().FirstOrDefaultAsync(u => u.Email == userEmail);
            }
        }

        public async Task RemoveSystemUsers(IEnumerable<UserFinanceSystem> users)
        {
            using (var database = new ApplicationContext(_optionsBuilder))
            {
                database.UsuarioSistemaFinanceiro.RemoveRange(users);
                await database.SaveChangesAsync();
            }
        }
    }
}
