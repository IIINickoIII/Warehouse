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
    public class OrderItemController : Controller
    {
        public OrderItemController(IOrderItemService orderItemService, IOrderService orderService, IItemService itemService, IMapper mapper)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _itemService = itemService;
            _mapper = mapper;
        }

        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        [Route("/OrderItems")]
        public IActionResult GetAllOrderItems(int orderId)
        {
            return View("OrderItemList", _orderItemService.GetAllOrderItemsForOrder(orderId) ?? new List<OrderItemDto>());
        }

        [Route("/OrderItems/{orderItemId}/Details")]
        public IActionResult OrderItemDetails(int orderItemId)
        {
            var orderItem = _orderItemService.GetOrderItem(orderItemId) ?? new OrderItemDto();
            var orderItemViewModel = _mapper.Map<OrderItemViewModel>(orderItem);
            return View("OrderItemDetails", orderItemViewModel);
        }

        [Route("/OrderItems/New")]
        public IActionResult CreateOrderItem(int orderId)
        {
            var orderItem = new OrderItemViewModel
            {
                OrderId = orderId,
                AllItemList = _itemService.GetAllItems()
            };
            return View("OrderItemForm", orderItem);
        }

        [Route("/OrderItems/{orderItemId}/Edit")]
        public IActionResult EditOrderItem(int orderItemId)
        {
            var orderItem = _orderItemService.GetOrderItem(orderItemId) ?? new OrderItemDto();
            var orderItemViewModel = _mapper.Map<OrderItemViewModel>(orderItem);
            orderItemViewModel.AllItemList = _itemService.GetAllItems();
            return View("OrderItemForm", orderItemViewModel);
        }

        [Route("/OrderItems/{orderItemId}/Delete")]
        public IActionResult DeleteOrderItem(int orderItemId)
        {
            var orderItem = _orderItemService.GetOrderItem(orderItemId);
            _orderItemService.SoftDelete(orderItemId);
            return RedirectToAction("EditOrder", "Order", new { orderId = orderItem.OrderId });
        }

        [HttpPost]
        public IActionResult Save(OrderItemViewModel orderItemViewModel)
        {
            if (!ModelState.IsValid)
            {
                orderItemViewModel.AllItemList = _itemService.GetAllItems();
                return View("OrderItemForm", orderItemViewModel);
            }

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItemViewModel);

            if (orderItemDto.Id == 0)
            {
                _orderItemService.AddOrderItem(orderItemDto);
            }
            else
            {
                _orderItemService.EditOrderItem(orderItemDto);
            }

            return RedirectToAction("EditOrder", "Order", new { orderId = orderItemDto.OrderId });
        }
    }
}
