using Domain.Interfaces.ICategory;
using Domain.Interfaces.ServiceInterfaces;
using Entities.Entities;

namespace Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategory _ICategory;

        public CategoryService(ICategory iCategory)
        {
            _ICategory = iCategory;
        }

        
        public async Task AdicionarCategoria(Category category)
        {
            var valid = category.ValidateStringProperty(category.Name, "Nome");
            if (valid)
            {
                await _ICategory.Add(category);
            }
        }

        public async Task AtualizarCategoria(Category category)
        {
            var valid = category.ValidateStringProperty(category.Name, "Nome");
            if (valid)
            {
                await _ICategory.Update(category);
            }
        }
    }
}
