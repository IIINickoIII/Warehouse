using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Warehouse.Bll.Interfaces;
using Warehouse.Web.Controllers;
using Warehouse.Web.Mapper;
using Xunit;

namespace Warehouse.Web.Tests.Controllers
{
    public class ProducerControllerTests
    {
        public ProducerControllerTests()
        {
            _producerService = new Mock<IProducerService>();
            _mapper = new MapperConfiguration(mc => { mc.AddProfile<MapperProfileWeb>(); }).CreateMapper();
        }

        private readonly Mock<IProducerService> _producerService;
        private readonly IMapper _mapper;

        [Fact]
        public void GetAllProducersViewResultNotNull()
        {
            // Arrange
            ProducerController controller = new ProducerController(_producerService.Object, _mapper);
            // Act
            ViewResult result = controller.GetAllProducers() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProducerDetailsViewResultNotNull()
        {
            // Arrange
            ProducerController controller = new ProducerController(_producerService.Object, _mapper);
            var producerId = 1;
            // Act
            ViewResult result = controller.ProducerDetails(producerId) as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateProducerViewResultNotNull()
        {
            // Arrange
            ProducerController controller = new ProducerController(_producerService.Object, _mapper);
            // Act
            ViewResult result = controller.CreateProducer() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void EditProducerViewResultNotNull()
        {
            // Arrange
            ProducerController controller = new ProducerController(_producerService.Object, _mapper);
            var producerId = 1;
            // Act
            ViewResult result = controller.EditProducer(producerId) as ViewResult;
            // Assert
            Assert.NotNull(result);
        }
    }
}
