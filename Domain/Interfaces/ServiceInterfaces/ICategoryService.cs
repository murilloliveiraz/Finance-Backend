using Entities.Entities;

namespace Domain.Interfaces.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task AdicionarCategoria(Category category);
        Task AtualizarCategoria(Category category);
    }
}
