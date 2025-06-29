using DineMasterApi.Data;
using DineMasterApi.DTO;
using DineMasterApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DineMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        ApplicationDbContext db;
        public DeliveryController(ApplicationDbContext db) 
        {
            this.db = db;
        }

        [HttpPost]
        [Route("AddDeliveryAddress")]
        public async Task<IActionResult> AddDeliveryAddress(CreateDeliveryAddDTO dto)
        {
            var data = new DeliveryAddress()
            {
                UserId = dto.UserId,
                FullAddress = dto.FullAddress,
                Pincode = dto.Pincode,
                City = dto.City,
                State = dto.State,
                Landmark = dto.Landmark
            };
            await db.DeliveryAddresses.AddAsync(data);
            await db.SaveChangesAsync();

            return Ok("Delivery Address Addedd Successfully");
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllDeliveryAddresses()
        {
            var data = await db.DeliveryAddresses.ToListAsync();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetDeliveryAddressById(int id)
        {
            var address = await db.DeliveryAddresses.FindAsync(id);
            if (address == null)
                return NotFound("Address not found");

            var dto = new FetchDeliveryAddDTO
            {
                AddressId = address.AddressId,
                UserId = address.UserId,
                FullAddress = address.FullAddress,
                City = address.City,
                State = address.State,
                Pincode = address.Pincode,
                
            };

            return Ok(dto);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateDeliveryAddress([FromBody] FetchDeliveryAddDTO dto)
        {
            var data = await db.DeliveryAddresses
                                         .FirstOrDefaultAsync(x => x.AddressId == dto.AddressId);

            if (data == null) 
                return NotFound("Address not found");

            data.FullAddress = dto.FullAddress;
            data.City = dto.City;
            data.State = dto.State;
            data.Pincode = dto.Pincode;
          
            await db.SaveChangesAsync();
            return Ok("Delivery address updated successfully.");
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteDeliveryAddress(int id)
        {
            var data = await db.DeliveryAddresses.FindAsync(id);
            if (data == null)
                return NotFound("Address not found");

            db.DeliveryAddresses.Remove(data);
            await db.SaveChangesAsync();

            return Ok("Delivery address deleted successfully.");
        }
    }
}
    

