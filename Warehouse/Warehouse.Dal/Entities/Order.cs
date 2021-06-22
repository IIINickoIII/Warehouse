using System;
using System.Collections.Generic;

namespace Warehouse.Dal.Entities
{
    public class Order : BaseEntity
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public bool IsPayed { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsClosedForEdit { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
