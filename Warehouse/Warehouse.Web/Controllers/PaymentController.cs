using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Warehouse.Bll.Dtos;
using Warehouse.Bll.Interfaces;
using Warehouse.Web.ViewModels;

namespace Warehouse.Web.Controllers
{
    public class PaymentController : Controller
    {
        public PaymentController(IPaymentService patmentService, IClientService clientService, IMapper mapper)
        {
            _paymentService = patmentService;
            _clientService = clientService;
            _mapper = mapper;
        }


        private readonly IPaymentService _paymentService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        [Route("/Payments")]
        public IActionResult GetAllPayments()
        {
            return View("PaymentList", _paymentService.GetAllPayments() ?? new List<PaymentDto>());
        }

        [Route("/Payments/{paymentId}/Details")]
        public IActionResult PaymentDetails(int paymentId)
        {
            var payment = _paymentService.GetPayment(paymentId) ?? new PaymentDto();
            var paymentViewModel = _mapper.Map<PaymentViewModel>(payment);
            return View("PaymentDetails", paymentViewModel);
        }

        [Route("/Payments/New")]
        public IActionResult CreatePayment()
        {
            var paymentViewModel = new PaymentViewModel
            {
                Date = DateTime.UtcNow,
                AllClientList = _clientService.GetAllClients()
            };
            return View("PaymentForm", paymentViewModel);
        }

        [Route("/Payments/{paymentId}/Edit")]
        public IActionResult EditPayment(int paymentId)
        {
            var payment = _paymentService.GetPayment(paymentId) ?? new PaymentDto();
            var paymentViewModel = _mapper.Map<PaymentViewModel>(payment);
            paymentViewModel.AllClientList = _clientService.GetAllClients();
            return View("PaymentForm", paymentViewModel);
        }

        [Route("/Payments/{paymentId}/Delete")]
        public IActionResult DeletePayment(int paymentId)
        {
            _paymentService.SoftDelete(paymentId);
            return RedirectToAction("GetAllPayments");
        }

        [HttpPost]
        public IActionResult Save(PaymentViewModel paymentViewModel)
        {
            if (!ModelState.IsValid)
            {
                paymentViewModel.AllClientList = _clientService.GetAllClients();
                return View("PaymentForm", paymentViewModel);
            }

            var paymentDto = _mapper.Map<PaymentDto>(paymentViewModel);

            if (paymentDto.Id == 0)
            {
                _paymentService.AddPayment(paymentDto);
            }
            else
            {
                _paymentService.EditPayment(paymentDto);
            }

            return RedirectToAction("GetAllPayments");
        }
    }
}
