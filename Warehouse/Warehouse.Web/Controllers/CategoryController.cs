using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Warehouse.Bll.Dtos;
using Warehouse.Bll.Interfaces;
using Warehouse.Web.ViewModels;

namespace Warehouse.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        [Route("/Categories")]
        public IActionResult GetAllCategories()
        {
            return View("CategoryList", _categoryService.GetAllCategories() ?? new List<CategoryDto>());
        }

        [Route("/Categories/{categoryId}/Details")]
        public IActionResult CategoryDetails(int categoryId)
        {
            var category = _categoryService.GetCategory(categoryId) ?? new CategoryDto();
            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            return View("CategoryDetails", categoryViewModel);
        }

        [Route("/Categories/New")]
        public IActionResult CreateCategory()
        {
            return View("CategoryForm", new CategoryViewModel());
        }

        [Route("/Categories/{categoryId}/Edit")]
        public IActionResult EditCategory(int categoryId)
        {
            var category = _categoryService.GetCategory(categoryId) ?? new CategoryDto();
            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            return View("CategoryForm", categoryViewModel);
        }

        [Route("/Categories/{categoryId}/Delete")]
        public IActionResult DeleteCategory(int categoryId)
        {
            _categoryService.SoftDelete(categoryId);
            return RedirectToAction("GetAllCategories");
        }

        [HttpPost]
        public IActionResult Save(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoryForm", categoryViewModel);
            }

            var categoryDto = _mapper.Map<CategoryDto>(categoryViewModel);

            if (categoryDto.Id == 0)
            {
                _categoryService.AddCategory(categoryDto);
            }
            else
            {
                _categoryService.EditCategory(categoryDto);
            }

            return RedirectToAction("GetAllCategories");
        }
    }
}
