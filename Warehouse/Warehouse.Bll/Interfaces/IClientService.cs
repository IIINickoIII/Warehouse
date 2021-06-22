using System.Collections.Generic;
using Warehouse.Bll.Dtos;

namespace Warehouse.Bll.Interfaces
{
    public interface IClientService
    {
        void AddClient(ClientDto clientDto);
        void EditClient(ClientDto clientDto);
        ClientDto GetClient(int clientDtoId);
        IEnumerable<ClientDto> GetAllClients();
        IEnumerable<ClientDto> GetAllCustomerClients();
        IEnumerable<ClientDto> GetAllProviderClients();
        void SoftDelete(int clientId);
    }
}
