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
    public class ProducerController : Controller
    {
        public ProducerController(IProducerService producerService, IMapper mapper)
        {
            _producerService = producerService;
            _mapper = mapper;
        }

        private readonly IProducerService _producerService;
        private readonly IMapper _mapper;

        [Route("/Producers")]
        public IActionResult GetAllProducers()
        {
            return View("ProducerList", _producerService.GetAllProducers() ?? new List<ProducerDto>());
        }

        [Route("/Producers/{producerId}/Details")]
        public IActionResult ProducerDetails(int producerId)
        {
            var producer = _producerService.GetProducer(producerId) ?? new ProducerDto();
            var producerViewModel = _mapper.Map<ProducerViewModel>(producer);
            return View("ProducerDetails", producerViewModel);
        }

        [Route("/Producers/New")]
        public IActionResult CreateProducer()
        {
            return View("ProducerForm", new ProducerViewModel());
        }

        [Route("/Producers/{producerId}/Edit")]
        public IActionResult EditProducer(int producerId)
        {
            var producer = _producerService.GetProducer(producerId) ?? new ProducerDto();
            var producerViewModel = _mapper.Map<ProducerViewModel>(producer);
            return View("ProducerForm", producerViewModel);
        }

        [Route("/Producers/{producerId}/Delete")]
        public IActionResult DeleteProducer(int producerId)
        {
            _producerService.SoftDelete(producerId);
            return RedirectToAction("GetAllProducers");
        }

        [HttpPost]
        public IActionResult Save(ProducerViewModel producerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProducerForm", producerViewModel);
            }

            var producerDto = _mapper.Map<ProducerDto>(producerViewModel);

            if (producerDto.Id == 0)
            {
                _producerService.AddProducer(producerDto);
            }
            else
            {
                _producerService.EditProducer(producerDto);
            }

            return RedirectToAction("GetAllProducers");
        }
    }
}
