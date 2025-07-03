using DineMasterApi.DTO;
using DineMasterApi.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DineMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        IReservationRepo repo;
        public ReservationController(IReservationRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost("AddReservation")]
        public async Task<IActionResult> AddReservation(ReservationDTO1 dto)
        {
            await repo.AddReservationAsync(dto);
            return Ok(new { message = "Added Successfully" });
        }

        [HttpGet("FetchReservation")]
        public async Task<IActionResult> FetchReservation()
        {
            var data = await repo.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("GetReservation/{id}")]
        public async Task<IActionResult> GetReservation(int id)
        {
            var data = await repo.GetReservationByIdAsync(id);
            return Ok(data);
        }

        [HttpPut("UpdateReservation")]
        public async Task<IActionResult> UpdateReservation(ReservationDTO3 dto)
        {
            await repo.UpdateReservationAsync(dto);
            return Ok(new { message = "Updated Successfully" });
        }

        [HttpDelete("DeleteReservation/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            int r = await repo.DeleteReservationAsync(id);
            if (r > 0)
            {
                return Ok(new { message = "Deleted Successfully" });
            }
            else
            {
                return NotFound(new { message = "Cannot delete this record" });
            }
        }

        [HttpGet("GetAvailableTable")]
        public async Task<IActionResult> GetAvailableTable()
        {
            var data = await repo.GetAvailableTableAsync();
            return Ok(data);
        }

        [HttpPost("GetSuitableTable")]
        public async Task<IActionResult> GetSuitableTable(ReservationDTO4 dto)
        {
            if (dto.StartTime >= dto.EndTime)
            {
                return NotFound(new { message = "End time must be after start time." });
            }
            var data = await repo.GetSuitableTableAsync(dto);
            if (data.Count == 0)
                return NotFound(new { message = "No suitable tables available for the selected time slot." });
            return Ok(data);
        }

        [HttpPut("CheckIn/{id}")]
        public async Task<IActionResult> CheckIn(int id)
        {
            bool result = await repo.CheckInAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Invalid reservation or already seated" });
            }

            return Ok(new { message = "Checked in successfully" });
        }

        [HttpPut("CheckOut/{id}")]
        public async Task<IActionResult> CheckOut(int id)
        {
            bool result = await repo.CheckOutAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Invalid reservation or already completed" });
            }
            return Ok(new { message = "Checked out successfully" });
        }

    }
}
