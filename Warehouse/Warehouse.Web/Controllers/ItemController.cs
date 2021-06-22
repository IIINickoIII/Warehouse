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
    public class ItemController : Controller
    {
        public ItemController(IItemService itemService, IProducerService producerService, ICategoryService categoryService, IMapper mapper)
        {
            _itemService = itemService;
            _producerService = producerService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        private readonly IItemService _itemService;
        private readonly IProducerService _producerService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        [Route("/Items")]
        public IActionResult GetAllItems()
        {
            return View("ItemList", _itemService.GetAllItems() ?? new List<ItemDto>());
        }

        [Route("/Items/{itemId}/Details")]
        public IActionResult ItemDetails(int itemId)
        {
            var item = _itemService.GetItem(itemId) ?? new ItemDto();
            var itemViewModel = _mapper.Map<ItemViewModel>(item);
            return View("ItemDetails", itemViewModel);
        }

        [Route("/Items/New")]
        public IActionResult CreateItem()
        {
            var itemViewModel = new ItemViewModel
            {
                AllProducerList = _producerService.GetAllProducers(),
                AllCategoryList = _categoryService.GetAllCategories()
            };
            return View("ItemForm", itemViewModel);
        }

        [Route("/Items/{itemId}/Edit")]
        public IActionResult EditItem(int itemId)
        {
            var item = _itemService.GetItem(itemId) ?? new ItemDto();
            var itemViewModel = _mapper.Map<ItemViewModel>(item);
            itemViewModel.AllProducerList = _producerService.GetAllProducers();
            itemViewModel.AllCategoryList = _categoryService.GetAllCategories();
            return View("ItemForm", itemViewModel);
        }

        [Route("/Items/{itemId}/Delete")]
        public IActionResult DeleteItem(int itemId)
        {
            _itemService.SoftDelete(itemId);
            return RedirectToAction("GetAllItems");
        }

        [HttpPost]
        public IActionResult Save(ItemViewModel itemViewModel)
        {
            if (!ModelState.IsValid)
            {
                itemViewModel.AllProducerList = _producerService.GetAllProducers();
                itemViewModel.AllCategoryList = _categoryService.GetAllCategories();
                return View("ItemForm", itemViewModel);
            }

            var itemDto = _mapper.Map<ItemDto>(itemViewModel);

            if (itemDto.Id == 0)
            {
                _itemService.AddItem(itemDto);
            }
            else
            {
                _itemService.EditItem(itemDto);
            }

            return RedirectToAction("GetAllItems");
        }
    }
}
