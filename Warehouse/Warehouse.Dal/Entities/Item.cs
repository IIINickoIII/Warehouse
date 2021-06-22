using System.ComponentModel.DataAnnotations;

namespace Warehouse.Dal.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int UnitInStock { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
