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
            CreateMap<Inventory, InventoryDto>().ReverseMap();
            CreateMap<InventoryCreateDto, Inventory>(); // ✅ This was missing
            CreateMap<inventoryUpdateDto, Inventory>(); // ✅ Fix name case too if needed
            CreateMap<RecipeItem, RecipeItemDto>()
              .ForMember(dest => dest.InventoryItemName, opt => opt.MapFrom(src => src.Inventory.ItemName))
              .ForMember(dest => dest.IsLowStock, opt => opt.MapFrom(src => src.Inventory.Quantity < src.QuantityNeeded))
              .ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
        }
    }
}
