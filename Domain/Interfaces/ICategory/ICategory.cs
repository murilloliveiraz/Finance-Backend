using Domain.Interfaces.Generics;
using Entities.Entities;

namespace Domain.Interfaces.ICategory
{
    public interface ICategory : GenericInterface<Category>
    {
        Task<IEnumerable<Category>> GetUserCategories(string userEmail);
    }
}
