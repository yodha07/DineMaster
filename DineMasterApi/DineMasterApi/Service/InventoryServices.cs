using AutoMapper;
using DineMasterApi.Data;
using DineMasterApi.DTO;
using DineMasterApi.Models;
using DineMasterApi.Repo;
using Microsoft.EntityFrameworkCore;

namespace DineMasterApi.Service
{
    public class InventoryServices:IInventory
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public InventoryServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<InventoryDto> AddInventoryAsyn(InventoryCreateDto inventoryCreateDto)
        {
            var data = _mapper.Map<Inventory>(inventoryCreateDto);
            data.LastUpdated = DateTime.Now;
            data.isActive = true;
            await _context.Inventory.AddAsync(data);
            await _context.SaveChangesAsync();
            return _mapper.Map<InventoryDto>(data);

        }

        public async Task<IEnumerable<InventoryDto>> GetInventoryAsyn()
        {
            var data = await _context.Inventory.Where(x => x.isActive).ToListAsync();
            return _mapper.Map<IEnumerable<InventoryDto>>(data);
        }
        public async Task<InventoryDto> UpdateInventoryAsync(int id, inventoryUpdateDto dto)
        {
            var item = await _context.Inventory.FindAsync(id);
            if (item == null || !item.isActive) return null;

            item.ItemName = dto.ItemName;
            item.Quantity = dto.Quantity;
            item.Unit = dto.Unit;
            item.ReorderLevel = dto.ReorderReorderLevel;
            item.LastUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
            return _mapper.Map<InventoryDto>(item);
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            var item = await _context.Inventory.FindAsync(id);
            if (item == null || !item.isActive) return false;

            item.isActive = false;
            item.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
