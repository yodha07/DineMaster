using DineMasterApi.DTO;
using DineMasterApi.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DineMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeItemController : ControllerBase
    {
        public readonly IRecipeitem _inv;
        public RecipeItemController(IRecipeitem inv)
        {
            _inv = inv;

        }

        [HttpPost]
        [Route("AddRecipeitem")]
        public async Task<IActionResult> AddRecipeitem([FromBody] RecipeItemDto items)
        {
            var data = await _inv.AddRecipeitemAsync(items);
            return Ok(data);

        }
        [HttpGet]
        [Route("GetRecipeitem")]
        public async Task<IActionResult> GetRecipeitem()
        {
            var data = await _inv.GetAllRecipeitemsAsync();
            return Ok(data);

        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _inv.DeleteRecipeItemAsync(id);
            if (!result)
                return NotFound();
            return Ok("Deleted");
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _inv.GetRecipeItemByIdAsync(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RecipeItemDto item)
        {
            var data = await _inv.UpdateRecipeItemAsync(id, item);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

    }
}
