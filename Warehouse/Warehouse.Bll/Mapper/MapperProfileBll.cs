using AutoMapper;
using Warehouse.Bll.Dtos;
using Warehouse.Dal.Entities;

namespace Warehouse.Bll.Mapper
{
    public class MapperProfileBll : Profile
    {
        public MapperProfileBll()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Producer, ProducerDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
