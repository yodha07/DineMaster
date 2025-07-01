using DineMasterApi.DTO;
using DineMasterApi.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DineMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        ITableRepo repo;
        public TableController(ITableRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost("AddTable")]
        public async Task<IActionResult> AddTable(TableDTO1 dto)
        {
            await repo.AddTableAsync(dto);
            return Ok(new { message = "Added Successfully" });
        }

        [HttpGet("FetchTable")]
        public async Task<IActionResult> FetchTable()
        {
            var data = await repo.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("GetTable/{id}")]
        public async Task<IActionResult> GetTable(int id)
        {
            var data = await repo.GetTableByIdAsync(id);
            return Ok(data);
        }

        [HttpPut("UpdateTable")]
        public async Task<IActionResult> UpdateTable(TableDTO3 dto)
        {
            await repo.UpdateTableAsync(dto);
            return Ok(new { message = "Updated Successfully" });
        }

        [HttpDelete("DeleteTable/{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            int r = await repo.DeleteTableAsync(id);
            if (r > 0)
            {
                return Ok(new { message = "Deleted Successfully" });
            }
            else
            {
                return NotFound(new { message = "Cannot delete this record" });
            }
        }
    }
}
