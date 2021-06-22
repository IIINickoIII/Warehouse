namespace Warehouse.Dal.Entities
{
    public class Producer : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
