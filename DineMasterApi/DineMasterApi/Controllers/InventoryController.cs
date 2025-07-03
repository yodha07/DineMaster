using DineMasterApi.DTO;
using DineMasterApi.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DineMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        public IInventory _inv;
        public InventoryController(IInventory inv)
        {
            _inv = inv;
        }

        [HttpPost]
        [Route("AddInventory")]
        public async Task<IActionResult> AddInventory(InventoryCreateDto inventory)
        {
            var data = await _inv.AddInventoryAsyn(inventory);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetAllInventory")]
        public async Task<IActionResult> GetAllInventory()
        {
            var result = await _inv.GetInventoryAsyn();
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, inventoryUpdateDto dto)
        {
            var updated = await _inv.UpdateInventoryAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var deleted = await _inv.DeleteInventoryAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Deleted successfully" });
        }
    }
}
