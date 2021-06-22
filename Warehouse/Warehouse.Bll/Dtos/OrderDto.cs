using System;
using System.Collections.Generic;

namespace Warehouse.Bll.Dtos
{
    public class OrderDto : BaseDto
    {
        public int ClientId { get; set; }
        public ClientDto Client { get; set; }
        public bool IsPayed { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsClosedForEdit { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
