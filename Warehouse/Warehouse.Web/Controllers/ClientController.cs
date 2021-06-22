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
    public class ClientController : Controller
    {
        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        [Route("/Clients")]
        public IActionResult GetAllClients()
        {
            return View("ClientList", _clientService.GetAllClients() ?? new List<ClientDto>());
        }

        [Route("/Clients/Customers")]
        public IActionResult GetAllCustomerClients()
        {
            return View("CustomerClientList", _clientService.GetAllCustomerClients() ?? new List<ClientDto>());
        }

        [Route("/Clients/Providers")]
        public IActionResult GetAllProviderClients()
        {
            return View("ProviderClientList", _clientService.GetAllProviderClients() ?? new List<ClientDto>());
        }

        [Route("/Clients/{clientId}/Details")]
        public IActionResult ClientDetails(int clientId)
        {
            var client = _clientService.GetClient(clientId) ?? new ClientDto();
            var clientViewModel = _mapper.Map<ClientViewModel>(client);
            return View("ClientDetails", clientViewModel);
        }

        [Route("/Clients/New")]
        public IActionResult CreateClient()
        {
            return View("ClientForm", new ClientViewModel());
        }

        [Route("/Client/{clientId}/Edit")]
        public IActionResult EditClient(int clientId)
        {
            var client = _clientService.GetClient(clientId) ?? new ClientDto();
            var clientViewModel = _mapper.Map<ClientViewModel>(client);
            return View("ClientForm", clientViewModel);
        }

        [Route("/Clients/{clientId}/Delete")]
        public IActionResult DeleteClient(int clientId)
        {
            _clientService.SoftDelete(clientId);
            return RedirectToAction("GetAllClients");
        }

        [HttpPost]
        public IActionResult Save(ClientViewModel clientViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ClientForm", clientViewModel);
            }

            var clientDto = _mapper.Map<ClientDto>(clientViewModel);

            if (clientDto.Id == 0)
            {
                _clientService.AddClient(clientDto);
            }
            else
            {
                _clientService.EditClient(clientDto);
            }

            return RedirectToAction("GetAllClients");
        }
    }
}
