namespace DineMasterApi.DTO
{
    public class InventoryCreateDto
    {
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal ReorderReorderLevel { get; set; }
    }
}
