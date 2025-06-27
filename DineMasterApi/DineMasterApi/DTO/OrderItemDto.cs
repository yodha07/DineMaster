namespace DineMasterApi.DTO
{
    public class OrderItemDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public string MenuItemName { get; set; }
        public decimal ItemPrice { get; set; }

    }
}
