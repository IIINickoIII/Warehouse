using Warehouse.Dal.Entities;

namespace Warehouse.Dal.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Category> CategoryRepository { get; }
        IBaseRepository<Client> ClientRepository { get; }
        IBaseRepository<Item> ItemRepository { get; }
        IBaseRepository<Order> OrderRepository { get; }
        IBaseRepository<OrderItem> OrderItemRepository { get; }
        IBaseRepository<Producer> ProducerRepository { get; }
        IBaseRepository<Payment> PaymentRepository { get; }
        void Save();
    }
}
