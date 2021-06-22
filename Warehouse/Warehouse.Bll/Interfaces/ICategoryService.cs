using System.Collections.Generic;
using Warehouse.Bll.Dtos;

namespace Warehouse.Bll.Interfaces
{
    public interface ICategoryService
    {
        void AddCategory(CategoryDto categoryDto);
        void EditCategory(CategoryDto categoryDto);
        CategoryDto GetCategory(int categoryId);
        IEnumerable<CategoryDto> GetAllCategories();
        void SoftDelete(int categoryId);
    }
}
