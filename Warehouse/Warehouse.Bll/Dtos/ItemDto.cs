namespace Warehouse.Bll.Dtos
{
    public class ItemDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int UnitInStock { get; set; }
        public int ProducerId { get; set; }
        public ProducerDto Producer { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
