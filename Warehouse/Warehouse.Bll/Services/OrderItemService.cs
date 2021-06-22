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
    public class OrderItemService : IOrderItemService
    {
        public OrderItemService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
            _includes = orderItems => orderItems
                .Include(orderItem => orderItem.Order)
                .Include(orderItem => orderItem.Item);
        }

        private readonly Func<IQueryable<OrderItem>, IIncludableQueryable<OrderItem, object>> _includes;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public void AddOrderItem(OrderItemDto orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            var sumWithoutDiscount = orderItem.Price * orderItem.Quantity;
            sumWithoutDiscount = sumWithoutDiscount - sumWithoutDiscount % 0.01M;
            var discount = sumWithoutDiscount * orderItem.Discount;
            var sumWithDiscount = sumWithoutDiscount - discount;
            sumWithDiscount = sumWithDiscount - sumWithDiscount % 0.01M;
            orderItem.SumWithoutDiscount = sumWithoutDiscount;
            orderItem.SumWithDiscount = sumWithDiscount;

            _uow.OrderItemRepository.Update(orderItem);
            _uow.Save();

            var relatedOrder = _uow.OrderRepository.GetById(orderItemDto.OrderId);
            var relatedOrderItems = _uow.OrderItemRepository.Find(i => i.IsDeleted == false & i.OrderId == orderItemDto.OrderId);
            relatedOrder.TotalSum = relatedOrderItems.Select(i => i.SumWithDiscount).Sum();

            _uow.OrderRepository.Update(relatedOrder);
            _uow.Save();
        }

        public void EditOrderItem(OrderItemDto orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            var sumWithoutDiscount = orderItem.Price * orderItem.Quantity;
            sumWithoutDiscount = sumWithoutDiscount - sumWithoutDiscount % 0.01M;
            var discount = sumWithoutDiscount * orderItem.Discount;
            var sumWithDiscount = sumWithoutDiscount - discount;
            sumWithDiscount = sumWithDiscount - sumWithDiscount % 0.01M;
            orderItem.SumWithoutDiscount = sumWithoutDiscount;
            orderItem.SumWithDiscount = sumWithDiscount;

            _uow.OrderItemRepository.Update(orderItem);
            _uow.Save();

            var relatedOrder = _uow.OrderRepository.GetById(orderItemDto.OrderId);
            var relatedOrderItems = _uow.OrderItemRepository.Find(i => i.IsDeleted == false & i.OrderId == orderItemDto.OrderId);
            relatedOrder.TotalSum = relatedOrderItems.Select(i => i.SumWithDiscount).Sum();

            _uow.OrderRepository.Update(relatedOrder);
            _uow.Save();
        }

        public IEnumerable<OrderItemDto> GetAllOrderItemsForOrder(int orderId)
        {
            var orderItemsInDb = _uow.OrderItemRepository.Find(o => o.OrderId == orderId & o.IsDeleted == false, _includes);
            var orderItemDtos = _mapper.Map<IEnumerable<OrderItemDto>>(orderItemsInDb);
            return orderItemDtos;
        }

        public OrderItemDto GetOrderItem(int orderItemDtoId)
        {
            var orderItemInDb = _uow.OrderItemRepository.Single(o => o.Id == orderItemDtoId, _includes);
            var orderItemDto = _mapper.Map<OrderItemDto>(orderItemInDb);
            return orderItemDto;
        }

        public void SoftDelete(int orderItemDtoId)
        {
            var orderItemInDb = _uow.OrderItemRepository.Single(o => o.Id == orderItemDtoId);
            _uow.OrderItemRepository.SoftDelete(orderItemInDb);
            _uow.Save();
        }
    }
}
