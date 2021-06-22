using System.Collections.Generic;
using Warehouse.Bll.Dtos;

namespace Warehouse.Bll.Interfaces
{
    public interface IPaymentService
    {
        void AddPayment(PaymentDto paymentDto);
        void EditPayment(PaymentDto paymentDto);
        PaymentDto GetPayment(int paymentDtoId);
        IEnumerable<PaymentDto> GetAllPayments();
        void SoftDelete(int paymentDtoId);
    }
}
