namespace Warehouse.Dal.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal SumWithoutDiscount { get; set; }
        public decimal SumWithDiscount { get; set; }
    }
}
