namespace DineMasterApi.DTO
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
