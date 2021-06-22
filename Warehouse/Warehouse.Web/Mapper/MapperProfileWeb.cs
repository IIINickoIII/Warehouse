using AutoMapper;
using Warehouse.Bll.Dtos;
using Warehouse.Web.ViewModels;

namespace Warehouse.Web.Mapper
{
    public class MapperProfileWeb : Profile
    {
        public MapperProfileWeb()
        {
            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<ClientDto, ClientViewModel>().ReverseMap();
            CreateMap<ItemDto, ItemViewModel>().ReverseMap();
            CreateMap<OrderDto, OrderViewModel>().ReverseMap();
            CreateMap<OrderItemDto, OrderItemViewModel>().ReverseMap();
            CreateMap<ProducerDto, ProducerViewModel>().ReverseMap();
            CreateMap<PaymentDto, PaymentViewModel>().ReverseMap();
        }
    }
}
