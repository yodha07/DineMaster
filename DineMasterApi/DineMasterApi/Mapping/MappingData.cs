using AutoMapper;
using DineMasterApi.DTO;
using DineMasterApi.Models;

namespace DineMasterApi.Mapping
{
    public class MappingData : Profile
    {
        public MappingData()
        {
            CreateMap<Order, OrderDto>()
    .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.DiningTable.TableName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name));
            CreateMap<Bill, BillDto>().ReverseMap();
        }
    }
}
