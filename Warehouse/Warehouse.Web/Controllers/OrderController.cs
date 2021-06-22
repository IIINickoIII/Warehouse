using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Warehouse.Bll.Dtos;
using Warehouse.Bll.Interfaces;
using Warehouse.Web.ViewModels;

namespace Warehouse.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IPdfCreatorService _pdfCreatorService;
        private readonly IOrderItemService _orderItemService;
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private const string FileType = "pdf";
        private const string FileMime = "application/pdf";
        private const string FileName = "Order Details";

        public OrderController(IPdfCreatorService pdfCreatorService, IOrderItemService orderItemService, IClientService clientService, IOrderService orderService, IMapper mapper)
        {
            _pdfCreatorService = pdfCreatorService;
            _orderItemService = orderItemService;
            _clientService = clientService;
            _orderService = orderService;
            _mapper = mapper;
        }

        [Route("/Orders")]
        public IActionResult GetAllOrders()
        {
            return View("OrderList", _orderService.GetAllOrders() ?? new List<OrderDto>());
        }

        [Route("/Incomes")]
        public IActionResult GetAllIncomes()
        {
            return View("IncomeList", _orderService.GetAllIncomes() ?? new List<OrderDto>());
        }

        [Route("/Orders/New")]
        public IActionResult CreateOrder()
        {
            var orderViewModel = new OrderViewModel
            {
                Date = DateTime.UtcNow,
                AllClientList = _clientService.GetAllClients(),
                OrderItems = new List<OrderItemDto>()
            };
            return View("OrderForm", orderViewModel);
        }

        [Route("/Orders/{orderId}/Edit")]
        public IActionResult EditOrder(int orderId)
        {
            var orderDto = _orderService.GetOrder(orderId);
            var orderViewModel = _mapper.Map<OrderViewModel>(orderDto);
            orderViewModel.AllClientList = _clientService.GetAllClients();
            orderViewModel.OrderItems = _orderItemService.GetAllOrderItemsForOrder(orderId);
            return View("OrderForm", orderViewModel);
        }

        [Route("/Order/{orderId}/Delete")]
        public IActionResult DeleteOrder(int orderId)
        {
            var order = _orderService.GetOrder(orderId);
            _orderService.SoftDelete(orderId);

            if (order.IsIncome)
            {
                return RedirectToAction("GetAllIncomes");
            }

            return RedirectToAction("GetAllPayments");
        }

        [HttpPost]
        public IActionResult Save(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                orderViewModel.AllClientList = _clientService.GetAllClients();
                if(orderViewModel.Id != 0)
                {
                    orderViewModel.OrderItems = _orderItemService.GetAllOrderItemsForOrder(orderViewModel.Id);
                }
                else
                {
                    orderViewModel.OrderItems = new List<OrderItemDto>();
                }
                
                return View("PaymentForm", orderViewModel);
            }

            var orderDto = _mapper.Map<OrderDto>(orderViewModel);

            if (orderDto.Id == 0)
            {
                orderDto.Id = _orderService.AddOrder(orderDto);
            }
            else
            {
                _orderService.EditOrder(orderDto);
            }

            return RedirectToAction("EditOrder", new { orderId = orderDto.Id });
        }

        [Route("/Order/{orderId}/Download")]
        public IActionResult DownloadOrder(int orderId)
        {
            var order = _orderService.GetOrder(orderId);
            order.OrderItems = _orderItemService.GetAllOrderItemsForOrder(orderId);
            var fileStreamResult = new FileStreamResult(
                _pdfCreatorService.CreateStream(_pdfCreatorService.GenerateInvoiceContentPdf(order)),
                new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(FileMime))
            {
                FileDownloadName = $"{FileName}.{FileType}"
            };
            return fileStreamResult;
        }
    }
}
