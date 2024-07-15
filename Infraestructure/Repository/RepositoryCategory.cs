using Domain.Interfaces.ICategory;
using Entities.Entities;
using Infraestructure.Configurations;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class RepositoryCategory : RepositoryGeneric<Category>, ICategory
    {
        private readonly DbContextOptions<ApplicationContext> _optionsBuilder;
        public RepositoryCategory()
        {
            _optionsBuilder = new DbContextOptions<ApplicationContext>();
        }
        public async Task<IEnumerable<Category>> GetUserCategories(string userEmail)
        {
            using (var database = new ApplicationContext(_optionsBuilder))
            {
                return await
                    (from s in database.SistemaFinanceiro
                     join c in database.Categoria on s.Id equals c.IdSystem
                     join us in database.UsuarioSistemaFinanceiro on s.Id equals us.IdSystem
                     where us.Email.Equals(userEmail) && us.CurrentSystem
                     select c).AsNoTracking().ToListAsync();
            }
        }
    }
}
