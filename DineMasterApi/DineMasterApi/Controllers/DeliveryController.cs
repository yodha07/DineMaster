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

            return Ok( new {message= "Delivery Address Addedd Successfully" });
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


        [HttpPut]
        [Route("status/{orderId}")]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            var order = await db.Orders.FindAsync(orderId);
            if (order == null) return NotFound();

            db.DeliveryTrackings.Add(new DeliveryTracking
            {
                OrderId = orderId,
                Status = status,
                UpdatedAt = DateTime.Now
            });

            if (status == "Out for Delivery")
            {
                var otp = new Random().Next(100000, 999999).ToString();
                HttpContext.Session.SetString($"OTP_{orderId}", otp);
                Console.WriteLine($"OTP for Order #{orderId}: {otp}");
            }

            await db.SaveChangesAsync();
            return Ok($"Status '{status}' added.");
        }

        [HttpGet]
        [Route ("status/latest/{orderId}")]
        public async Task<IActionResult> GetLatestStatus(int orderId)
        {
            var status = await db.DeliveryTrackings.Where(x => x.OrderId == orderId).
                OrderByDescending(x => x.UpdatedAt).FirstOrDefaultAsync();

            if(status == null) return NotFound();
            return Ok(status);
        }

        [HttpPost]
        [Route("verify-delivery")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpVerifyDTO dto)
        {
            var order = await db.Orders.FindAsync(dto.OrderId);
            if (order == null) return NotFound();

            var sessionOtp = HttpContext.Session.GetString($"OTP_{dto.OrderId}");

            if (sessionOtp == null) return BadRequest("OTP expired or not generated.");

            if (sessionOtp == dto.Otp)
            {
                order.OrderStatus = "Delivered";
                HttpContext.Session.Remove($"OTP_{dto.OrderId}");

                await db.SaveChangesAsync();
                return Ok("OTP verified. Order marked as delivered.");
            }

            return BadRequest("Invalid OTP.");
        }

    }
}
    

