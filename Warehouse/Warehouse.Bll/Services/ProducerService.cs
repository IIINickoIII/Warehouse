using AutoMapper;
using System.Collections.Generic;
using Warehouse.Bll.Dtos;
using Warehouse.Bll.Interfaces;
using Warehouse.Dal.Entities;
using Warehouse.Dal.Interfaces;

namespace Warehouse.Bll.Services
{
    public class ProducerService : IProducerService
    {
        public ProducerService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public void AddProducer(ProducerDto producerDto)
        {
            var producer = _mapper.Map<Producer>(producerDto);
            _uow.ProducerRepository.Add(producer);
            _uow.Save();
        }

        public void EditProducer(ProducerDto producerDto)
        {
            var producer = _mapper.Map<Producer>(producerDto);
            _uow.ProducerRepository.Update(producer);
            _uow.Save();
        }

        public IEnumerable<ProducerDto> GetAllProducers()
        {
            var producersInDb = _uow.ProducerRepository.GetAll();
            var producerDtos = _mapper.Map<IEnumerable<ProducerDto>>(producersInDb);
            return producerDtos;
        }

        public ProducerDto GetProducer(int producerDtoId)
        {
            var producerInDb = _uow.ProducerRepository.Single(p => p.Id == producerDtoId);
            var producerDto = _mapper.Map<ProducerDto>(producerInDb);
            return producerDto;
        }

        public void SoftDelete(int producerId)
        {
            var producer = _uow.ProducerRepository.Single(p => p.Id == producerId);
            _uow.ProducerRepository.SoftDelete(producer);
            _uow.Save();
        }
    }
}
