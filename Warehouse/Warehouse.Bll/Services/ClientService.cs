using AutoMapper;
using System.Collections.Generic;
using Warehouse.Bll.Dtos;
using Warehouse.Bll.Interfaces;
using Warehouse.Dal.Entities;
using Warehouse.Dal.Interfaces;

namespace Warehouse.Bll.Services
{
    public class ClientService : IClientService
    {
        public ClientService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public void AddClient(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            _uow.ClientRepository.Add(client);
            _uow.Save();
        }

        public void EditClient(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            _uow.ClientRepository.Update(client);
            _uow.Save();
        }

        public IEnumerable<ClientDto> GetAllClients()
        {
            var clientsInDb = _uow.ClientRepository.GetAll();
            var clientDtos = _mapper.Map<IEnumerable<ClientDto>>(clientsInDb);
            return clientDtos;
        }

        public ClientDto GetClient(int clientDtoId)
        {
            var clientInDb = _uow.ClientRepository.Single(c => c.Id == clientDtoId);
            var clientDto = _mapper.Map<ClientDto>(clientInDb);
            return clientDto;
        }

        public void SoftDelete(int clientId)
        {
            var client = _uow.ClientRepository.Single(c => c.Id == clientId);
            _uow.ClientRepository.SoftDelete(client);
            _uow.Save();
        }

        public IEnumerable<ClientDto> GetAllCustomerClients()
        {
            var clientsInDb = _uow.ClientRepository
                .Find(c => c.IsCustomer == true);
            var clientDtos = _mapper.Map<IEnumerable<ClientDto>>(clientsInDb);
            return clientDtos;
        }

        public IEnumerable<ClientDto> GetAllProviderClients()
        {
            var clientsInDb = _uow.ClientRepository
                .Find(c => c.IsProvider == true);
            var clientDtos = _mapper.Map<IEnumerable<ClientDto>>(clientsInDb);
            return clientDtos;
        }
    }
}
