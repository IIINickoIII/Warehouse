using System.Collections.Generic;
using Warehouse.Bll.Dtos;

namespace Warehouse.Bll.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(OrderDto orderDto);
        void EditOrder(OrderDto orderDto);
        OrderDto GetOrder(int orderDtoId);
        IEnumerable<OrderDto> GetAllOrders();
        IEnumerable<OrderDto> GetAllIncomes();
        void SoftDelete(int orderDtoId);
    }
}
