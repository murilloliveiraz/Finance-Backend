using Domain.Interfaces.Generics;
using Infraestructure.Configurations;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infraestructure.Repository.Generics
{
    public class RepositoryGeneric<T> : GenericInterface<T> where T : class
    {
        private readonly DbContextOptions<ApplicationContext> _optionsBuilder;
        public RepositoryGeneric()
        {
            _optionsBuilder = new DbContextOptions<ApplicationContext>();
        }
        public async Task Add(T entity)
        {
            using (var data = new ApplicationContext(_optionsBuilder))
            {
                data.Set<T>().AddAsync(entity);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T entity)
        {
            using (var data = new ApplicationContext(_optionsBuilder))
            {
                data.Set<T>().Remove(entity);
                await data.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> Get()
        {
            using (var data = new ApplicationContext(_optionsBuilder))
            {
                return await data.Set<T>().ToListAsync();
            }
        }

        public async Task<T> GetEntityById(int id)
        {
            using (var data = new ApplicationContext(_optionsBuilder))
            {
                return await data.Set<T>().FindAsync(id);
            }
        }

        public async Task Update(T entity)
        {
            using (var data = new ApplicationContext(_optionsBuilder))
            {
                data.Set<T>().Update(entity);
                await data.SaveChangesAsync();
            }
        }
    }
}
