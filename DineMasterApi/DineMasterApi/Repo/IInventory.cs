using DineMasterApi.DTO;

namespace DineMasterApi.Repo
{
    public interface IInventory
    {
        Task<InventoryDto> AddInventoryAsyn(InventoryCreateDto inventoryCreateDto);

        Task<IEnumerable<InventoryDto>> GetInventoryAsyn();

        Task<InventoryDto> UpdateInventoryAsync(int id, inventoryUpdateDto dto);
        Task<bool> DeleteInventoryAsync(int id);
    }
}
