using AutoMapper;
using System.Collections.Generic;
using Warehouse.Bll.Dtos;
using Warehouse.Bll.Interfaces;
using Warehouse.Dal.Entities;
using Warehouse.Dal.Interfaces;

namespace Warehouse.Bll.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public void AddCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _uow.CategoryRepository.Add(category);
            _uow.Save();
        }

        public void EditCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _uow.CategoryRepository.Update(category);
            _uow.Save();
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categoriesInDb = _uow.CategoryRepository.GetAll();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categoriesInDb);
            return categoryDtos;
        }

        public CategoryDto GetCategory(int categoryId)
        {
            var categoryInDb = _uow.CategoryRepository.Single(c => c.Id == categoryId);
            var categoryDto = _mapper.Map<CategoryDto>(categoryInDb);
            return categoryDto;
        }

        public void SoftDelete(int categoryId)
        {
            var categoryInDb = _uow.CategoryRepository.Single(p => p.Id == categoryId);
            _uow.CategoryRepository.SoftDelete(categoryInDb);
            _uow.Save();
        }
    }
}
