using System.Collections.Generic;
using Warehouse.Bll.Dtos;

namespace Warehouse.Bll.Interfaces
{
    public interface IProducerService
    {
        void AddProducer(ProducerDto producerDto);
        void EditProducer(ProducerDto producerDto);
        ProducerDto GetProducer(int producerDtoId);
        IEnumerable<ProducerDto> GetAllProducers();
        void SoftDelete(int producerId);
    }
}
