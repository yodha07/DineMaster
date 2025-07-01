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

            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));


            CreateMap<Table, TableDTO1>().ReverseMap();
            CreateMap<Table, TableDTO2>().ReverseMap();
            CreateMap<Table, TableDTO3>().ReverseMap();

            CreateMap<Reservation, ReservationDTO1>().ReverseMap();
            CreateMap<Reservation, ReservationDTO2>()
            .ForMember(dest => dest.Tname, opt => opt.MapFrom(src => src.Table != null ? src.Table.Name : "")).ReverseMap();
            CreateMap<Reservation, ReservationDTO3>().ReverseMap();
        }
    }
}
