using System;

namespace Warehouse.Bll.Dtos
{
    public class PaymentDto : BaseDto
    {
        public DateTime Date { get; set; }
        public decimal TotalSum { get; set; }
        public int ClientId { get; set; }
        public ClientDto Client { get; set; }
        public bool IsInCome { get; set; }
    }
}
