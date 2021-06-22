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
    public class OrderService : IOrderService
    {
        public OrderService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
            _includes = orders => orders
                .Include(order => order.Client);
        }

        private readonly Func<IQueryable<Order>, IIncludableQueryable<Order, object>> _includes;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public int AddOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _uow.OrderRepository.Add(order);
            _uow.Save();

            return order.Id;
        }

        public void EditOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _uow.OrderRepository.Update(order);
            _uow.Save();
        }

        public IEnumerable<OrderDto> GetAllOrders()
        {
            var ordersInDb = _uow.OrderRepository.Find(o => o.IsIncome == false, _includes);
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(ordersInDb);
            return orderDtos;
        }

        public IEnumerable<OrderDto> GetAllIncomes()
        {
            var ordersInDb = _uow.OrderRepository.Find(o => o.IsIncome == true, _includes);
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(ordersInDb);
            return orderDtos;
        }

        public OrderDto GetOrder(int orderDtoId)
        {
            var orderInDb = _uow.OrderRepository.Single(o => o.Id == orderDtoId, _includes);
            var orderDto = _mapper.Map<OrderDto>(orderInDb);
            var relatedOrderItems = _uow.OrderItemRepository.Find(i => i.IsDeleted == false & i.OrderId == orderDtoId);
            orderDto.TotalSum = relatedOrderItems.Select(i => i.SumWithDiscount).Sum();

            return orderDto;
        }

        public void SoftDelete(int orderDtoId)
        {
            var orderInDb = _uow.OrderRepository.Single(o => o.Id == orderDtoId);
            _uow.OrderRepository.SoftDelete(orderInDb);
            _uow.Save();
        }
    }
}
