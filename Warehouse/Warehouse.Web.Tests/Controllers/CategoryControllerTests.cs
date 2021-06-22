using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Warehouse.Bll.Interfaces;
using Warehouse.Web.Controllers;
using Warehouse.Web.Mapper;
using Xunit;

namespace Warehouse.Web.Tests.Controllers
{
    public class CategoryControllerTests
    {
        public CategoryControllerTests()
        {
            _categoryService = new Mock<ICategoryService>();
            _mapper = new MapperConfiguration(mc => { mc.AddProfile<MapperProfileWeb>(); }).CreateMapper();
        }

        private readonly Mock<ICategoryService> _categoryService;
        private readonly IMapper _mapper;

        [Fact]
        public void GetAllCategoriesViewResultNotNull()
        {
            // Arrange
            CategoryController controller = new CategoryController(_categoryService.Object, _mapper);
            // Act
            ViewResult result = controller.GetAllCategories() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CategoryDetailsViewResultNotNull()
        {
            // Arrange
            CategoryController controller = new CategoryController(_categoryService.Object, _mapper);
            var categoryId = 1;
            // Act
            ViewResult result = controller.CategoryDetails(categoryId) as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateCategoryViewResultNotNull()
        {
            // Arrange
            CategoryController controller = new CategoryController(_categoryService.Object, _mapper);
            // Act
            ViewResult result = controller.CreateCategory() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void EditCategoryViewResultNotNull()
        {
            // Arrange
            CategoryController controller = new CategoryController(_categoryService.Object, _mapper);
            var categoryId = 1;
            // Act
            ViewResult result = controller.EditCategory(categoryId) as ViewResult;
            // Assert
            Assert.NotNull(result);
        }
    }
}
