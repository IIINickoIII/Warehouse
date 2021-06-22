using Warehouse.Dal.Contexts;
using Warehouse.Dal.Entities;
using Warehouse.Dal.Interfaces;

namespace Warehouse.Dal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(WarehouseContext context)
        {
            _context = context;
        }

        private readonly WarehouseContext _context;

        private IBaseRepository<Category> _categoryRepository;
        private IBaseRepository<Client> _clientRepository;
        private IBaseRepository<Item> _itemRepository;
        private IBaseRepository<Order> _orderRepository;
        private IBaseRepository<OrderItem> _orderItemRepository;
        private IBaseRepository<Producer> _producerRepository;
        private IBaseRepository<Payment> _paymentRepository;

        public IBaseRepository<Category> CategoryRepository
            => _categoryRepository ??= new BaseRepository<Category>(_context);

        public IBaseRepository<Client> ClientRepository
            => _clientRepository ??= new BaseRepository<Client>(_context);

        public IBaseRepository<Item> ItemRepository
            => _itemRepository ??= new BaseRepository<Item>(_context);

        public IBaseRepository<Order> OrderRepository
            => _orderRepository ??= new BaseRepository<Order>(_context);

        public IBaseRepository<OrderItem> OrderItemRepository
            => _orderItemRepository ??= new BaseRepository<OrderItem>(_context);

        public IBaseRepository<Producer> ProducerRepository
            => _producerRepository ??= new BaseRepository<Producer>(_context);
        public IBaseRepository<Payment> PaymentRepository
            => _paymentRepository ??= new BaseRepository<Payment>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
