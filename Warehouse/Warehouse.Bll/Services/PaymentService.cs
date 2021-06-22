using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Bll.Dtos;
using Warehouse.Bll.Interfaces;
using Warehouse.Dal.Entities;
using Warehouse.Dal.Interfaces;

namespace Warehouse.Bll.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
            _includes = payments => payments
                .Include(payment => payment.Client);
        }

        private readonly Func<IQueryable<Payment>, IIncludableQueryable<Payment, object>> _includes;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public void AddPayment(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            _uow.PaymentRepository.Add(payment);
            _uow.Save();
        }

        public void EditPayment(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            _uow.PaymentRepository.Update(payment);
            _uow.Save();
        }

        public IEnumerable<PaymentDto> GetAllPayments()
        {
            var paymentsInDb = _uow.PaymentRepository.GetAll(_includes);
            var paymentDtos = _mapper.Map<IEnumerable<PaymentDto>>(paymentsInDb);
            return paymentDtos;
        }

        public PaymentDto GetPayment(int paymentDtoId)
        {
            var paymentInDb = _uow.PaymentRepository.Single(p => p.Id == paymentDtoId, _includes);
            var paymentDto = _mapper.Map<PaymentDto>(paymentInDb);
            return paymentDto;
        }

        public void SoftDelete(int paymentDtoId)
        {
            var payment = _uow.PaymentRepository.Single(p => p.Id == paymentDtoId, _includes);
            _uow.PaymentRepository.SoftDelete(payment);
            _uow.Save();
        }
    }
}
