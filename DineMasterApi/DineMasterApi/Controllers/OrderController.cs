using AutoMapper;
using DineMasterApi.Data;
using DineMasterApi.DTO;
using DineMasterApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DineMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public OrderController(ApplicationDbContext db, IMapper mapper)
        {
            this.mapper = mapper;
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO dto)
        {
            var user = await db.Users.FindAsync(dto.UserId);
            if (user == null) return BadRequest("Invald user id");

            var order = new Order
            {
                UserId = dto.UserId,
                OrderType = dto.OrderType,
                TableId = dto.TableId,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                OrderItems = new List<OrderItem>()
            };

            foreach(var item in dto.OrderItems)
            {
                var menuItem = await db.MenuItems.FindAsync(item.MenuItemId);
                if (menuItem == null) return BadRequest($"Menu item with id {menuItem} not available");

                order.OrderItems.Add(new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    ItemPrice = menuItem.Price,
                });
            }

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return Ok(new
            {
                order.OrderId,
                Message = "Order placed successfully"
            });
        }

        [HttpPost("{orderId}/bill")]
        public async Task<IActionResult> GenerateBill(int orderId, GenerateBillRequestDto req)
        {
            var order = await db.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null) return NotFound("Order not found");

            var billG = await db.Bills.Where(b => b.OrderId == orderId).FirstOrDefaultAsync();
            if (billG != null) return BadRequest("Bill already generated");

            decimal subtotal = order.OrderItems.Sum(item => item.ItemPrice * item.Quantity);

            decimal tax = subtotal * 0.18m; //18% gst
            decimal discount = 0;
            decimal total = subtotal + tax - discount;

            var billDto = new BillDto
            {
                OrderId = orderId,
                BillDate = DateTime.Now,
                Subtotal = subtotal,
                Tax = tax,
                Discount = discount,
                TotalAmount = total,
                PaymentMethod = req.PaymentMethod
            };
            
                var bill = mapper.Map<Bill>(billDto);
                db.Bills.Add(bill);
                await db.SaveChangesAsync();
                var billRes = mapper.Map<BillDto>(bill);
                return Ok(billRes);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await db.Orders.Include(o => o.OrderItems)
                .ThenInclude(i => i.MenuItem)
                .Include(o => o.Bill)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null) return NotFound("Order not found");

            var orderDto = new OrderDetailsDto
            {
                OrderId = order.OrderId,
                TableId = (int)order.TableId,
                Status = order.OrderStatus,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(i => new OrderItemDto
                {
                    MenuItemId = i.MenuItemId,
                    MenuItemName = i.MenuItem.Name,
                    ItemPrice = i.ItemPrice,
                    Quantity = i.Quantity
                }).ToList(),
                Bill = order.Bill == null ? null : new BillDto
                {
                    BillDate = order.Bill.BillDate,
                    Subtotal = order.Bill.Subtotal,
                    Tax = order.Bill.Tax,
                    Discount = order.Bill.Discount,
                    TotalAmount = order.Bill.TotalAmount,
                    PaymentMethod = order.Bill.PaymentMethod
                }
            };

            return Ok(orderDto);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await db.Orders.Include(o => o.OrderItems).ThenInclude(i => i.MenuItem).Include(o => o.Bill).Include(o => o.DiningTable).ToListAsync();

            var orderDtos = mapper.Map<List<OrderDto>>(orders);
            return Ok(orderDtos);
        }
    }
}
