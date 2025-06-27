namespace DineMasterApi.DTO
{
    public class CreateOrderDTO
    {
        public int UserId { get; set; }
        public string OrderType { get; set; }  // "DineIn", "Takeaway", "Online"
        public int? TableId { get; set; } // optional
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
