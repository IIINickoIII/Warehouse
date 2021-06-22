namespace Warehouse.Bll.Dtos
{
    public class OrderItemDto : BaseDto
    {
        public int ItemId { get; set; }
        public ItemDto Item { get; set; }
        public int OrderId { get; set; }
        public OrderDto Order { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal SumWithoutDiscount { get; set; }
        public decimal SumWithDiscount { get; set; }
    }
}
