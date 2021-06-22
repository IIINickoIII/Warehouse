using System.Collections.Generic;
using Warehouse.Bll.Dtos;

namespace Warehouse.Bll.Interfaces
{
    public interface IOrderItemService
    {
        void AddOrderItem(OrderItemDto orderItemDto);
        void EditOrderItem(OrderItemDto orderItemDto);
        OrderItemDto GetOrderItem(int orderItemDtoId);
        IEnumerable<OrderItemDto> GetAllOrderItemsForOrder(int orderId);
        void SoftDelete(int orderItemDtoId);
    }
}
