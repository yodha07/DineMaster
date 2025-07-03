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
            try
            {
                var user = await db.Users.FindAsync(dto.UserId);
                if (user == null) return BadRequest("Invalid user id");

                var order = new Order
                {
                    UserId = dto.UserId,
                    OrderType = dto.OrderType,
                    TableId = dto.TableId,
                    OrderDate = DateTime.Now,
                    OrderStatus = "Pending",
                    OrderItems = dto.OrderItems.Select(item => new OrderItem
                    {
                        MenuItemId = item.MenuItemId,
                        Quantity = item.Quantity,
                        ItemPrice = db.MenuItems.FirstOrDefault(m => m.ItemId == item.MenuItemId)?.Price ?? 0
                    }).ToList()
                };

                db.Orders.Add(order);
                await db.SaveChangesAsync();

                return Ok(new
                {
                    order.OrderId,
                    Message = "Order placed successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost("{orderId}/bill")]
        public async Task<IActionResult> GenerateBill(int orderId, GenerateBillRequestDto req)
        
        {
            var order = await db.Orders.Include(o => o.OrderItems).Include(o => o.Bill)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

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

            var toDel = await db.CartItems.FirstOrDefaultAsync(c => c.UserId == 2);
            if (toDel != null)
            {
                db.CartItems.Remove(toDel);
            }
            //return Ok(new { billRes });
            return Ok(new { message = "Bill generated" });
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
                    BillId = order.Bill.BillId,
                    OrderId = order.Bill.OrderId,
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

        [HttpGet("orders/user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await db.Orders.Where(o => o.UserId == userId && o.Bill != null).Include(o => o.OrderItems).ThenInclude(i => i.MenuItem).Include(o => o.Bill).Include(o => o.DiningTable).ToListAsync();

            if (!orders.Any()) return NotFound("No orders found for this user");

            var orderDto = mapper.Map<List<OrderDto>>(orders);
            return Ok(orderDto);
        }

        [HttpGet("Allorders/user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserIdAll(int userId)
        {
            var orders = await db.Orders
    .Where(o => o.UserId == userId && o.Bill == null)
    .Include(o => o.OrderItems)
        .ThenInclude(i => i.MenuItem)
    .Include(o => o.Bill)
    .Include(o => o.DiningTable)
    .ToListAsync();

            if (!orders.Any()) return NotFound("No orders found for this user");

            var orderDto = mapper.Map<List<OrderDto>>(orders);
            return Ok(new { orderDto });
        }

        [HttpGet("orders/getMenu")]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var menuItems = await db.MenuItems
                .Include(m => m.Category)
                .Where(m => m.IsAvailable)
                .ToListAsync();

            var menuItemDtos = mapper.Map < List<MenuItemDto>>(menuItems);
            return Ok(menuItemDtos);
        }

        [HttpPost("cart/add")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            try
            {
                var cartItem = new CartItem
                {
                    UserId = dto.UserId,
                    MenuItemId = dto.MenuItemId,
                    Quantity = dto.Quantity
                };

                db.CartItems.Add(cartItem);
                await db.SaveChangesAsync();

                return Ok(new { Message = "Item added to cart" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }


        [HttpGet("cart/{userId}")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            var cartItems = await db.CartItems.Where(c => c.UserId == userId)
              .Include(c => c.MenuItem)
              .ToListAsync();

            var cartDtos = cartItems.Select(item => new CartItemDto
            {
                CartItemId = item.CartItemId,
                MenuItemId = item.MenuItemId,
                MenuItemName = item.MenuItem.Name,
                ItemPrice = item.MenuItem.Price,
                Quantity = item.Quantity,
                TotalPrice = item.MenuItem.Price * item.Quantity
            }).ToList();

            return Ok(cartDtos);
        }
    }
}
