using AutoMapper;
using DineMasterApi.Data;
using DineMasterApi.DTO;
using DineMasterApi.Models;
using DineMasterApi.Repo;
using Microsoft.EntityFrameworkCore;

namespace DineMasterApi.Service
{
    public class RecipeitemServices:IRecipeitem
    {

        public readonly ApplicationDbContext _Context;
        public readonly IMapper _mapper;
        public RecipeitemServices(ApplicationDbContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
        public async Task<RecipeItemDto> AddRecipeitemAsync(RecipeItemDto item)
        {
            var data = await _Context.Inventory.FindAsync(item.InventoryItemId);
            if (data == null || data.Quantity < item.QuantityNeeded)
            {
                throw new Exception("Insufficinet recipe for this inventory item");
            }
            var entity = new RecipeItem
            {
                MenuItemId = item.MenuItemId,
                InventoryItemId = item.InventoryItemId,
                QuantityNeeded = item.QuantityNeeded,
                Unit = item.Unit
            };
            await _Context.RecipeItems.AddAsync(entity);
            await _Context.SaveChangesAsync();
            item.RecipeItemId = entity.RecipeItemId;
            item.InventoryItemName = data.ItemName;
            item.IsLowStock = data.Quantity < item.QuantityNeeded;
            return item;



        }

        public async Task<IEnumerable<RecipeItemDto>> GetAllRecipeitemsAsync()
        {
            var data = await _Context.RecipeItems.Include(x => x.Inventory).Include(x => x.MenuItem).ToListAsync();
            var entities = data.Select(item => new RecipeItemDto
            {
                RecipeItemId = item.RecipeItemId,
                MenuItemId = item.MenuItemId,
                InventoryItemId = item.InventoryItemId,
                QuantityNeeded = item.QuantityNeeded,
                Unit = item.Unit,
                IsLowStock = item.Inventory.Quantity < item.QuantityNeeded
            });
            return entities;
        }

        public async Task<bool> DeleteRecipeItemAsync(int id)
        {
            var item = await _Context.RecipeItems.FindAsync(id);
            if (item == null) return false;

            _Context.RecipeItems.Remove(item);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<RecipeItemDto> GetRecipeItemByIdAsync(int id)
        {
            var item = await _Context.RecipeItems
                .Include(x => x.Inventory)
                .FirstOrDefaultAsync(x => x.RecipeItemId == id);

            if (item == null) return null;

            return new RecipeItemDto
            {
                RecipeItemId = item.RecipeItemId,
                MenuItemId = item.MenuItemId,
                InventoryItemId = item.InventoryItemId,
                QuantityNeeded = item.QuantityNeeded,
                Unit = item.Unit,
                InventoryItemName = item.Inventory.ItemName,
                IsLowStock = item.Inventory.Quantity < item.QuantityNeeded
            };
        }

        public async Task<RecipeItemDto> UpdateRecipeItemAsync(int id, RecipeItemDto item)
        {
            var existing = await _Context.RecipeItems.FindAsync(id);
            if (existing == null) return null;

            existing.MenuItemId = item.MenuItemId;
            existing.InventoryItemId = item.InventoryItemId;
            existing.QuantityNeeded = item.QuantityNeeded;
            existing.Unit = item.Unit;

            await _Context.SaveChangesAsync();

            var inventory = await _Context.Inventory.FindAsync(item.InventoryItemId);

            return new RecipeItemDto
            {
                RecipeItemId = existing.RecipeItemId,
                MenuItemId = existing.MenuItemId,
                InventoryItemId = existing.InventoryItemId,
                QuantityNeeded = existing.QuantityNeeded,
                Unit = existing.Unit,
                InventoryItemName = inventory?.ItemName,
                IsLowStock = inventory != null && inventory.Quantity < item.QuantityNeeded
            };
        }

    }
}
