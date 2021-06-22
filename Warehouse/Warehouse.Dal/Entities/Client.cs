namespace Warehouse.Dal.Entities
{
    public class Client : BaseEntity
    {
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsProvider { get; set; }
    }
}
