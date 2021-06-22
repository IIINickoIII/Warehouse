using System;

namespace Warehouse.Dal.Entities
{
    public class Payment : BaseEntity
    {
        public DateTime Date { get; set; }
        public decimal TotalSum { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public bool IsInCome { get; set; }
    }
}
