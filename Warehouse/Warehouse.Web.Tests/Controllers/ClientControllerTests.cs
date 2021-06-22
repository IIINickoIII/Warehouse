using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Warehouse.Bll.Interfaces;
using Warehouse.Web.Controllers;
using Warehouse.Web.Mapper;
using Xunit;

namespace Warehouse.Web.Tests.Controllers
{
    public class ClientControllerTests
    {
        public ClientControllerTests()
        {
            _clientService = new Mock<IClientService>();
            _mapper = new MapperConfiguration(mc => { mc.AddProfile<MapperProfileWeb>(); }).CreateMapper();
        }

        private readonly Mock<IClientService> _clientService;
        private readonly IMapper _mapper;

        [Fact]
        public void GetAllClientsViewResultNotNull()
        {
            // Arrange
            ClientController controller = new ClientController(_clientService.Object, _mapper);
            // Act
            ViewResult result = controller.GetAllClients() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllCustomerClientsViewResultNotNull()
        {
            // Arrange
            ClientController controller = new ClientController(_clientService.Object, _mapper);
            // Act
            ViewResult result = controller.GetAllCustomerClients() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllProviderViewResultNotNull()
        {
            // Arrange
            ClientController controller = new ClientController(_clientService.Object, _mapper);
            // Act
            ViewResult result = controller.GetAllProviderClients() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ClientDetailsViewResultNotNull()
        {
            // Arrange
            ClientController controller = new ClientController(_clientService.Object, _mapper);
            var clientId = 1;
            // Act
            ViewResult result = controller.ClientDetails(clientId) as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateClientViewResultNotNull()
        {
            // Arrange
            ClientController controller = new ClientController(_clientService.Object, _mapper);
            // Act
            ViewResult result = controller.CreateClient() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void EditClientViewResultNotNull()
        {
            // Arrange
            ClientController controller = new ClientController(_clientService.Object, _mapper);
            var clientId = 1;
            // Act
            ViewResult result = controller.EditClient(clientId) as ViewResult;
            // Assert
            Assert.NotNull(result);
        }
    }
}
